using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

	public float timer = 0;
	public float speed = 1;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(timer > speed){
			timer = 0F;
			GameObject arrow = (GameObject) GameObject.Instantiate(Resources.Load("Arrow"), transform.position + new Vector3(0, 0, 1), transform.rotation);
			arrow.transform.parent = transform;
		}
	
	}
}
