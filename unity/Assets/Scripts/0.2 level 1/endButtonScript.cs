using UnityEngine;
using System.Collections;

public class endButtonScript : MonoBehaviour {
	
	bool insi = false;
	GameObject platform01;
	GameObject player;
	bool buttonIn = false;
	float buttonInTimer = 0.0f;
	public string dir;
	Vector3 posBasic;
	Vector3 pos;
	bool done = false;
	bool lookIn = false;
	
	// Use this for initialization
	void Start () {
		posBasic = transform.position;
		pos = posBasic;
	}
	
	// Update is called once per frame
	void Update () {
		if(lookIn && insi && !player.GetComponent<pauseMenuScript>().paused && Input.GetButtonDown("Action")){
			buttonIn = true;
			Application.LoadLevel(0);
		}
		
		if(buttonIn){
			if(!done){
				switch(dir)
				{
				case "x-" :
					pos.x -= 0.03f;
					break;
				case "x+" :
					pos.x += 0.03f;
					break;
				case "y-" :
					pos.y -= 0.03f;
					break;
				case "y+" :
					pos.y += 0.03f;
					break;
				case "z-" :
					pos.z -= 0.03f;
					break;
				case "z+" :
					pos.z += 0.03f;
					break;
				}
			}
			done = true;
			transform.position = pos;
			buttonInTimer += Time.deltaTime;
			if(buttonInTimer > 0.3f){
				buttonInTimer = 0;
				buttonIn = false;
				Application.LoadLevel(0);
			}
		}
		else {
			pos = posBasic;
			transform.position = pos;
			done = false;
		}	
	}
	
	void OnTriggerEnter(Collider obj){
		if(obj.name == "Player")
//Debug.Log("Button IN");
		insi = true;
	}
	void OnTriggerExit(Collider obj){
		if(obj.name == "Player")
//Debug.Log("Button OUT");
		insi = false;
	}
	
	
	void OnMouseDown(){
		if(insi){
			Application.LoadLevel(0);
			buttonIn = true;
			
		}
	}
	void OnMouseEnter(){
		lookIn = true;
	}
	void OnMouseExit(){
		lookIn = false;	
	}
}
