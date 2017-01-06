using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	public float speed = 1;
	public float moveSpeed = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update (){
		transform.position -= transform.right * moveSpeed * Time.deltaTime;	
		
		if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.position) > 32){
			Destroy(gameObject);	
		}
	
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("ow");
		if(other.tag == "Player"){
			other.gameObject.GetComponent<PlayerScript>().Hurt();
			Destroy(gameObject);
		}
	}
	
}
