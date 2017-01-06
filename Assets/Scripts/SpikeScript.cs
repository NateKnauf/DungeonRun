using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {

	public float timer = 0;
	public float speed = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			other.gameObject.GetComponent<PlayerScript>().Hurt();
		}
	}
}
