using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	public float timer = 0;
	public float speed = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer % 1.5 < 0.75){
			GetComponent<SpriteRenderer>().enabled = true;
		}else{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			if(timer % 1.5 < 0.75){
				other.gameObject.GetComponent<PlayerScript>().Hurt();
			}
		}
	}
}