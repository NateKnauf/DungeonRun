using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	
	public int score = 0;

	public int zone = 0;
	public int stage = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = score + "00";
		if(score == 0){
			guiText.text = "0";	
		}
	}
}
