using UnityEngine;
using System.Collections;

public class LaserEndScript : MonoBehaviour {

	public float timer = 0;
	public float onTimer = 0;
	public float speed = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime*speed;
		onTimer += Time.deltaTime;
		
		for(int i=0;i<transform.childCount;i++){
			transform.GetChild(i).gameObject.SetActive(false);	
		}
		if(onTimer % 1.5 < 0.75){
			transform.FindChild(Mathf.FloorToInt(timer % 2 + 1).ToString()).gameObject.SetActive(true);
		}
		if(transform.childCount == 3){
			transform.FindChild("0").gameObject.SetActive(true);
		}
	
	}
}