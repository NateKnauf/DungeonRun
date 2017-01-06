using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	private Rigidbody2D control;
	
	public float xDir = 0;
	public float yDir = 0;
	public float speed = 0.1F;
	
	public int health = 5;
	public float invuln = 0F;
	
	public bool shielding = false;
	public float shield = 100F;
	
	public float shieldSpeed = 5F;
	public float shieldRegen = 1F;
	
	public GameObject gui;
	
	// Use this for initialization
	void Start () {
		control = rigidbody2D;
		gui = GameObject.Find("GUI");
	}
	
	// Update is called once per frame
	void Update () {
		invuln -= Time.deltaTime;
		
		xDir = Input.GetAxis("Horizontal");
		yDir = Input.GetAxis("Vertical");
		
		control.velocity = new Vector2(xDir*speed, yDir*speed);
		
		if(!(xDir == 0 && yDir == 0)){
			transform.FindChild("Texture").localEulerAngles = new Vector3(0, 0, 0);//Mathf.LerpAngle(transform.FindChild("Texture").localEulerAngles.z, Mathf.Atan2(control.velocity.x, control.velocity.z)/Mathf.PI*180F + 180, 0.125F));
		}
		if(invuln > 0){
			if(invuln % 0.125 > 0.0625){
				transform.FindChild("Texture").gameObject.SetActive(false);	
			}else{
				transform.FindChild("Texture").gameObject.SetActive(true);	
			}
		}else{
			//transform.FindChild("Texture").gameObject.SetActive(true);		
		}
		
		if(Input.GetKey(KeyCode.LeftShift)){
			if(shield > 0){
				shielding = true;
				shield -= Time.deltaTime*shieldSpeed;
			}else{
				shielding = false;	
			}
		}else{
			shielding = false;	
			shield += Time.deltaTime*shieldRegen;
		}
		shield = Mathf.Clamp(shield, 0F, 100F);
		
		if(shielding){
			if(shield > 0){
				transform.FindChild("Shield").gameObject.SetActive(true);	
			}else{
				transform.FindChild("Shield").gameObject.SetActive(false);	
			}
		}else{		
			transform.FindChild("Shield").gameObject.SetActive(false);
		}
		
		Rect px = GameObject.Find("Health").GetComponent<GUITexture>().pixelInset;
		GameObject.Find("Health").GetComponent<GUITexture>().pixelInset = new Rect(8 - (44 * (5-health)), px.y, px.width, px.height);
		
		Rect sx = GameObject.Find("ShieldBar").GetComponent<GUITexture>().pixelInset;
		GameObject.Find("ShieldBar").GetComponent<GUITexture>().pixelInset = new Rect(-216, px.y, shield/100F*208, px.height);
		
		Rect sbx = GameObject.Find("ShieldBack").GetComponent<GUITexture>().pixelInset;
		GameObject.Find("ShieldBack").GetComponent<GUITexture>().pixelInset = new Rect(-9, px.y, (100-shield)/100F*-208, px.height);
		

	}
	
	public void Hurt(){
		if(invuln < 0){
			if(!shielding){
				invuln = 1F;
				health--;
				rigidbody2D.angularVelocity = ((Random.Range(0, 2)*2)-1)*Random.Range(800F, 2000F);
				Debug.Log(rigidbody2D.angularVelocity);
			}
		}
		if(health <= 0){
			if(PlayerPrefs.HasKey("HighScore")){
				if(PlayerPrefs.GetInt("HighScore")<GameObject.Find("Score").GetComponent<ScoreScript>().score){
					PlayerPrefs.SetInt("HighScore", GameObject.Find("Score").GetComponent<ScoreScript>().score);
				}
			}else{
				PlayerPrefs.SetInt("HighScore", GameObject.Find("Score").GetComponent<ScoreScript>().score);
			}
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
