using UnityEngine;
using System.Collections;

public class ButtonScrip : MonoBehaviour {
	
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
	public string platformName01;

	// Use this for initialization
	void Start () {
		platform01 = GameObject.Find(platformName01);
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

			Action();
			
			buttonIn = true;
			
		}
	}
	void OnMouseEnter(){
		lookIn = true;
	}
	
	void OnMouseExit(){
		lookIn = false;	
	}
	
	void Action(){
		platform01.rigidbody.isKinematic = false;
		platform01.rigidbody.AddForce(100f, 0, 0);
	}
}
