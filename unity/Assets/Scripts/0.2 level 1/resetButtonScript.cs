using UnityEngine;
using System.Collections;

public class resetButtonScript : MonoBehaviour {
	
	
	GameObject platform01;
	GameObject player;
	GameObject platform02;
	public string platformName01;
	public string platformName02;
	Vector3 platform01Pos;
	Vector3 platform02Pos;
	bool buttonIn = false;
	float buttonInTimer = 0.0f;
	public string dir;
	Vector3 posBasic;
	Vector3 pos;
	bool done = false;
	bool insi = false;
	bool lookIn = false;
	
	// Use this for initialization
	void Start () {
		platform01 = GameObject.Find(platformName01);
		platform02 = GameObject.Find(platformName02);
		platform01Pos = platform01.transform.position;
		platform02Pos = platform02.transform.position;
		posBasic = transform.position;
		pos = posBasic;
	}
	
	// Update is called once per frame
	void Update () {
		if(lookIn && insi && !player.GetComponent<pauseMenuScript>().paused && Input.GetButtonDown("Action")){
			buttonIn = true;
			platform01.transform.position = platform01Pos;
			platform01.rigidbody.isKinematic = true;
			platform02.transform.position = platform02Pos;
			platform02.rigidbody.isKinematic = true;	
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
				done = true;
			}
			transform.position = pos;
			buttonInTimer += Time.deltaTime;
			if(buttonInTimer > 0.3f){
				buttonInTimer = 0;
				buttonIn = false;
			}
Debug.Log("Done");
		}
		else {
			pos = posBasic;
			transform.position = pos;
			done = false;
		}
	}
	
	void OnTriggerEnter(){
Debug.Log("Button IN");
		insi = true;
	}
	void OnTriggerExit(){
Debug.Log("Button OUT");
		insi = false;
	}
	
	
	void OnMouseDown(){
		if(insi){
			buttonIn = true;
Debug.Log("Clicked");
			platform01.transform.position = platform01Pos;
			platform01.rigidbody.isKinematic = true;
			platform02.transform.position = platform02Pos;
			platform02.rigidbody.isKinematic = true;	
			
		}
	}
	void OnMouseEnter(){
		lookIn = true;
	}
	void OnMouseExit(){
		lookIn = false;	
	}
}
