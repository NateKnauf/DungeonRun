using UnityEngine;
using System.Collections;

public class RoomGenScript : MonoBehaviour {
	
	public int enter = 0;
	public int exit = 0;
	
	public Texture2D prev;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Gen(){
		
		Debug.Log ("generating");
		
		exit = Random.Range(0, 4);
		if(exit == enter){
			exit += 2;
			if(exit > 3){
				exit -= 4;
			}
		}

		string newmap;

		if((enter + exit) % 2 == 0){ //if straight
			if(enter % 2 == 1){//if horz
				newmap = "Rooms/Horz/" + Random.Range(1,2).ToString();
				gameObject.GetComponent<Tilemap>().map = (Texture2D) Resources.Load(newmap);
				gameObject.GetComponent<Tilemap>().StartRoom(Random.Range(0,2)*2-1 , Random.Range(0,2)*2-1 );
			}else{//if vert
				newmap = "Rooms/Vert/" + Random.Range(1,2).ToString();
				gameObject.GetComponent<Tilemap>().map = (Texture2D) Resources.Load(newmap);
				gameObject.GetComponent<Tilemap>().StartRoom(Random.Range(0,2)*2-1 , Random.Range(0,2)*2-1 );
			}
		}else{ //if turn
			if(enter % 2 == 1){//if horz
				newmap = "Rooms/Turn/" + Random.Range(1,2).ToString();
				gameObject.GetComponent<Tilemap>().map = (Texture2D) Resources.Load(newmap);
				gameObject.GetComponent<Tilemap>().StartRoom(enter - 2, Random.Range(0,2)*2-1);
			}else{//if vert
				newmap = "Rooms/Turn/" + Random.Range(1,2).ToString();
				gameObject.GetComponent<Tilemap>().map = (Texture2D) Resources.Load(newmap);
				gameObject.GetComponent<Tilemap>().StartRoom(Random.Range(0,2)*2-1, enter - 1);
			}
		}
		
		Debug.Log(newmap);
		
	}
}
