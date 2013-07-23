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
Debug.Log(objectName);
		//platform01 = GameObject.Find(platformName01);
		player = GameObject.Find("Player");
		posBasic = transform.position;
		pos = posBasic;
	}
	
	// Update is called once per frame
	void Update () {
		// joystick push
		if(lookIn && insi && !player.GetComponent<pauseMenuScript>().paused && Input.GetButtonDown("Action")){
			buttonIn = true;
			Action();
		}
		
		// push in direction
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
		// reset button Position
		else {
			pos = posBasic;
			transform.position = pos;
			done = false;
		}
	}
	
	// in hitbox
	void OnTriggerEnter(Collider obj){
//Debug.Log("Button IN");
		if(obj.name == "Player") insi = true;
	}
	void OnTriggerExit(Collider obj){
//Debug.Log("Button OUT");
		if(obj.name == "Player") insi = false;
	}
	
	//click to
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
	
	//actual doing
	void Action(){
		targetObject = GameObject.Find(objectName);

		if(objectName[0] == 'P'){	// platform
			
			targetObject.rigidbody.isKinematic = true;
			targetObject.rigidbody.constraints = RigidbodyConstraints.None;
			targetObject.rigidbody.isKinematic = false;
			
			platformScript platScript = targetObject.GetComponent<platformScript>();
Debug.Log (objectDirection);
			platScript.movePlatform(objectDirection);
		}
		else if(objectName[0] == 'D'){ // door
			switch(objectDirection)
			{
			case "open" :
				targetObject.GetComponent<doorScript>().Open();
				break;
			case "close" :
				targetObject.GetComponent<doorScript>().Close();
				break;
			}
			
		}
	}
}
