using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	public float time = 10;
	
	void Start () {
	
	}
	
	void Update () {
		time -= Time.deltaTime;
		
		guiText.text = Mathf.Ceil(time) + "";
		
		if(time <= 0){
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().health = 0;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().Hurt();
		}
		
	}
}
