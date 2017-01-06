using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			GameObject.Find("Score").GetComponent<ScoreScript>().score += 3;
			Destroy(gameObject);
		}
	}
}
