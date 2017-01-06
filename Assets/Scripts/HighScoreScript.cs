using UnityEngine;
using System.Collections;

public class HighScoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.HasKey("HighScore")){
			GetComponent<TextMesh>().text = PlayerPrefs.GetInt("HighScore") + "00";
		}else{
			GetComponent<TextMesh>().text = "0";
		}
	}
}
