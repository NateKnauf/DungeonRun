using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	public int dir = 0;
	public Vector3 move;
	public Vector3 playerMove;
	
	private GameObject newRoom;
	private GameObject nextRoom;
	
	// Use this for initialization
	void Start () {
		switch(dir){
			case 0:
				move = new Vector3(0, 10, 0);
				playerMove = new Vector3(0, 1, 0);
				break;
			case 1:
				move = new Vector3(16, 0, 0);
				playerMove = new Vector3(1, 0, 0);
				break;
			case 2:
				move = new Vector3(0, -10, 0);
				playerMove = new Vector3(0, -1, 0);
				break;
			case 3:
				move = new Vector3(-16, 0, 0);
				playerMove = new Vector3(-1, 0, 0);
				break;
			default:
				move = new Vector3(0, -10, 0);
				playerMove = new Vector3(0, -1, 0);
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 getMove(int d){
		switch(d){
		case 0:
			return new Vector3(0, 10, 0);
			break;
		case 1:
			return new Vector3(16, 0, 0);
			break;
		case 2:
			return new Vector3(0, -10, 0);
			break;
		case 3:
			return new Vector3(-16, 0, 0);
			break;
		default:
			return new Vector3(0, -10, 0);
			break;
		}

	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
		if(other.tag == "Player"){
			
			other.transform.position = transform.position + playerMove*3/4;
			GameObject.FindGameObjectWithTag("MainCamera").transform.position += move;
			if(transform.parent.gameObject.GetComponent<Tilemap>().childRoom == null){
				newRoom = (GameObject) Instantiate(Resources.Load("Chunk"), transform.parent.position + move, Quaternion.identity);
				transform.parent.gameObject.GetComponent<Tilemap>().childRoom = newRoom;
				newRoom.name = "NewChunk";
				newRoom.tag = "CurrentRoom";
				newRoom.GetComponent<Tilemap>().usage = 1;
				newRoom.GetComponent<RoomGenScript>().enter = dir;
				newRoom.GetComponent<RoomGenScript>().prev = transform.parent.GetComponent<Tilemap>().map;
				newRoom.GetComponent<RoomGenScript>().Gen();
				nextRoom = (GameObject) Instantiate(Resources.Load("Chunk"), newRoom.transform.position + getMove(newRoom.GetComponent<RoomGenScript>().exit), Quaternion.identity);
				newRoom.GetComponent<Tilemap>().childRoom = nextRoom;
				nextRoom.name = "NextChunk";
				nextRoom.tag = "NextRoom";
				nextRoom.GetComponent<Tilemap>().usage = 2;
				nextRoom.GetComponent<RoomGenScript>().enter = newRoom.GetComponent<RoomGenScript>().exit;
				nextRoom.GetComponent<RoomGenScript>().prev = newRoom.GetComponent<Tilemap>().map;
				nextRoom.GetComponent<RoomGenScript>().Gen();
				Destroy(transform.parent.gameObject);
			}else{
				nextRoom = (GameObject) Instantiate(Resources.Load("Chunk"), newRoom.transform.position + getMove(newRoom.GetComponent<RoomGenScript>().exit), Quaternion.identity);
				newRoom.GetComponent<Tilemap>().childRoom = nextRoom;
				nextRoom.name = "NextChunk";
				nextRoom.tag = "NextRoom";
				nextRoom.GetComponent<Tilemap>().usage = 2;
				nextRoom.GetComponent<RoomGenScript>().enter = newRoom.GetComponent<RoomGenScript>().exit;
				nextRoom.GetComponent<RoomGenScript>().prev = newRoom.GetComponent<Tilemap>().map;
				nextRoom.GetComponent<RoomGenScript>().Gen();
				Destroy(transform.parent.gameObject);
			}
			
			GameObject.Find("Score").GetComponent<ScoreScript>().score += Mathf.CeilToInt(GameObject.Find("Timer").GetComponent<TimerScript>().time);
			GameObject.Find("Timer").GetComponent<TimerScript>().time = 10F;
			
		}
	}
}
