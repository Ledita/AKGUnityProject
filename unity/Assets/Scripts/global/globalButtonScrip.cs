using UnityEngine;
using System.Collections;

public class globalButtonScrip : MonoBehaviour {
	
	bool insi = false;
	GameObject player;
	bool buttonIn = false;
	float buttonInTimer = 0.0f;
	public string dir;
	Vector3 posBasic;
	Vector3 pos;
	bool done = false;
	bool lookIn = false;
	
	public string objectName;
	public string objectDirection;
	
	GameObject targetObject;

	// Use this for initialization
	void Start () {
		//platform01 = GameObject.Find(platformName01);
		player = GameObject.Find("Player");
		posBasic = transform.position;
		pos = posBasic;
	}
	
	// Update is called once per frame
	void Update () {
		if(lookIn && insi && !player.GetComponent<pauseMenuScript>().paused && Input.GetButtonDown("Action")){
			buttonIn = true;
			Action();
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
			}
		}
		else {
			pos = posBasic;
			transform.position = pos;
			done = false;
		}
	}
	
	void OnTriggerEnter(Collider obj){
//Debug.Log("Button IN");
		if(obj.name == "Player") insi = true;
	}
	void OnTriggerExit(Collider obj){
//Debug.Log("Button OUT");
		if(obj.name == "Player") insi = false;
	}
	
	
	void OnMouseDown(){
		if(insi && !player.GetComponent<pauseMenuScript>().paused){
			buttonIn = true;
			Action();
			
		}
	}
	void OnMouseEnter(){
		lookIn = true;
	}
	
	void OnMouseExit(){
		lookIn = false;	
	}
	
	void Action(){
		targetObject = GameObject.Find(objectName);

		if(objectName[0] == 'P'){	// platform
			
			targetObject.rigidbody.isKinematic = true;
			targetObject.rigidbody.constraints = RigidbodyConstraints.None;
			targetObject.rigidbody.isKinematic = false;
			
			switch(objectDirection)
			{
			case "north" : // x+
				targetObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				targetObject.rigidbody.AddForce(100f, 0, 0);
				break;
			case "south" : // x-
				targetObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				targetObject.rigidbody.AddForce(-100f, 0, 0);
				break;
			case "down" : // y-
				targetObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				player.rigidbody.AddForce(0, -100f, 0);
				targetObject.rigidbody.AddForce(0, -100f, 0);
				break;
			case "up" : // y+
				targetObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				player.rigidbody.AddForce(0, 100f, 0);
				targetObject.rigidbody.AddForce(0, 100f, 0);
				break;
			case "east" : // z-
				targetObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
				targetObject.rigidbody.AddForce(0, 0, -100f);
				break;
			case "west" : // z+
				targetObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
				targetObject.rigidbody.AddForce(0, 0, 100f);
				Debug.Log("action");
				break;
			}
		}
		else if(objectName[0] == 'd'){ // door
			switch(objectDirection)
			{
			case "open" :
				//open
				break;
			case "close" :
				//close
				break;
			}
		}
	}
}
