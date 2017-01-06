using UnityEngine;
using System.Collections;

public class Tilemap : MonoBehaviour {

	public int height = 12;
	public int width = 18;
	
	public float tilesize = 1F;

	public int stage = 0;
	
	public Texture2D map;
	
	public Material[] textures;

	public Color[,] mapData;

	private int scaleX = 0;
	private int scaleY = 0;

	public int usage = 0;
	public GameObject childRoom;
	
	// Use this for initialization
	void Start () {
		width = map.width;
		height = map.height;
	}
	
	// Update is called once per frame
	void Update () {
		
		/*switch(stage){
		case 1:
			readMap();
			break;
		case 2:
			buildMap();
			break;
		default:
			break;

		}*/
		
	}
	
	public void StartRoom(int x, int y){
		Debug.Log ("building room");
		
		for(int c=0;c<transform.childCount;c++){
			Destroy(transform.GetChild(c).gameObject);	
		}

		scaleX = x;
		scaleY = y;

		stage = 1;
		readMap();
		buildMap();

	}

	public void readMap(){
		stage = 2;
		
		mapData = new Color[width,height+1];
		
		int coX = 0;
		int coY = 0;

		for(int b=0;b<map.height;b++){
			coY = 0;
			if(scaleY > 0){
				coY = b;
			}else{
				coY = height-b;
			}
			for(int a=0;a<map.width;a++){
				coX = 0;
				if(scaleX > 0){
					coX = a;
				}else{
					coX = width-1-a;
				}

				//coY += 1;
				Debug.Log(coX + "," + coY);
				mapData[a,b] = map.GetPixel(coX, coY);
				mapData[a,b].r = Mathf.Round(mapData[a,b].r*2)/2;
				mapData[a,b].g = Mathf.Round(mapData[a,b].g*2)/2;
				mapData[a,b].b = Mathf.Round(mapData[a,b].b*2)/2; 
			}
		}
	}

	public void buildMap(){
		stage = 3;
		for(int y=0;y<height+1;y++){
			for(int x=0;x<width;x++){
				GameObject kid = gameObject;
				
				int rotates = 0;
				
				Color pixel = mapData[x,y];
				
				kid = (GameObject) Instantiate(Resources.Load("Floor"), new Vector3(
				transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
				transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
				1),
				Quaternion.Euler(new Vector3(0, 0, 0)));
				kid.name = "Floor";
				kid.transform.parent = transform;
				
				if(mapData[x,y].a == 1F){
					if(pixel == new Color(0,0,1,1)){
						kid = (GameObject) Instantiate(Resources.Load("Cube"), new Vector3(
						transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
						transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
						0),
					    Quaternion.Euler(new Vector3(0, 0, 0)));
						kid.name = "Cube";
						kid.transform.parent = transform;
					}else if(pixel == new Color(0,1,0,1)){
						Debug.Log("Door at " + x + ", " + y);
						if(y == 5){
							kid = (GameObject) Instantiate(Resources.Load("Door"), new Vector3(
							transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
							transform.position.y - (height/2*tilesize) + (y*tilesize),
							0),
							Quaternion.Euler(new Vector3(0, 0, 90)));
							kid.name = "Door";
							kid.transform.parent = transform;
							if(x == 0){
								kid.GetComponent<DoorScript>().dir = 3;
							}else{
								kid.GetComponent<DoorScript>().dir = 1;
							}
						}else if(x == 8){
							kid = (GameObject) Instantiate(Resources.Load("Door"), new Vector3(
							transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize,
							transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
							0),
                            Quaternion.Euler(new Vector3(0, 0, 0)));
							kid.name = "Door";
							kid.transform.parent = transform;
							if(y == 0){
								kid.GetComponent<DoorScript>().dir = 2;
							}else{
								kid.GetComponent<DoorScript>().dir = 0;
							}
						}
						
					}else if(pixel == new Color(1,1,0,1)){
						kid = (GameObject) Instantiate(Resources.Load("Coin"), new Vector3(
						transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
						transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
						0),
						Quaternion.Euler(new Vector3(0, 0, 0)));
						kid.name = "Coin";
						kid.transform.parent = transform;
					}else if(pixel == new Color(1,0,0,1)){
						kid = (GameObject) Instantiate(Resources.Load("LaserEnd"), new Vector3(
						transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
						transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
						-1),
						Quaternion.Euler(new Vector3(0, 0, 0)));
						kid.name = "LaserEnd";
						kid.transform.parent = transform;
						rotates = 2;
					}else if(pixel == new Color(1,0.5F,0,1)){
						kid = (GameObject) Instantiate(Resources.Load("Spikes"), new Vector3(
						transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
						transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
						0),
                        Quaternion.Euler(new Vector3(0, 0, 0)));
						kid.name = "Spikes";
						kid.transform.parent = transform;
					}else if(pixel == new Color(1,0,1,1)){
						kid = (GameObject) Instantiate(Resources.Load("ArrowShooter"), new Vector3(
						transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
						transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
						-1),
                        Quaternion.Euler(new Vector3(0, 0, 0)));
						kid.name = "ArrowShooter";
						kid.transform.parent = transform;
						rotates = 1;
					}else{
					}
					
					if(rotates > 0){
						int face = 0;
						
						if(mapData[x+1,y] == new Color(0, 0, 1, 1)){
							kid.transform.rotation = Quaternion.Euler(0, 0, 0);
							face = 3;
						}
						if(mapData[x-1,y] == new Color(0, 0, 1, 1)){
							kid.transform.rotation = Quaternion.Euler(0, 0, 180);
							face = 1;
							if(rotates == 2){
								kid.transform.localScale = new Vector3(1, -1, 1);
							}
						}
						if(mapData[x,y-1] == new Color(0, 0, 1, 1)){
							kid.transform.rotation = Quaternion.Euler(0, 0, 270);
							face = 4;
						}
						if(mapData[x,y+1] == new Color(0, 0, 1, 1)){
							kid.transform.rotation = Quaternion.Euler(0, 0, 90);
							face = 2;
							if(rotates == 2){
								kid.transform.localScale = new Vector3(1, -1, 1);
							}
						}
						if(rotates == 2){
							if(face == 0){
								
								Destroy(kid);
								
							}
							kid = (GameObject) Instantiate(Resources.Load("Laser"), new Vector3(
							transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
							transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
							0),
                            Quaternion.Euler(new Vector3(0, 0, 0)));
							kid.name = "Laser";
							kid.transform.parent = transform;
							
							int neighbors = 0;
							
							if(mapData[x+1,y] == new Color(1, 0, 0, 1)){
								neighbors += 1;
							}
							if(mapData[x-1,y] == new Color(1, 0, 0, 1)){
								neighbors += 2;
							}
							if(mapData[x,y-1] == new Color(1, 0, 0, 1)){
								neighbors += 4;
							}
							if(mapData[x,y+1] == new Color(1, 0, 0, 1)){
								neighbors += 8;
							}
							
							if(neighbors == 15 || neighbors == 14 || neighbors == 13 || neighbors == 11 || neighbors == 7){
								Destroy(kid);	
								kid = (GameObject) Instantiate(Resources.Load("MidLaser"), new Vector3(
								transform.position.x - (width/2*tilesize) + (x*tilesize) + tilesize/2,
								transform.position.y - (height/2*tilesize) + (y*tilesize) - tilesize/2,
								0),
		                        Quaternion.Euler(new Vector3(0, 0, 0)));
								kid.name = "MidLaser";
								kid.transform.parent = transform;
							}else if(neighbors == 12 || neighbors == 4 || neighbors == 8){
								kid.transform.rotation = Quaternion.Euler(0, 0, 270);
							}
						}
					}
					
				}
			}
		}
	}
	
}
