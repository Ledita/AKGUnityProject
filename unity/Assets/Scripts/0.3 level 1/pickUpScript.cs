using UnityEngine;
using System.Collections;

public class pickUpScript : MonoBehaviour {
	
	GameObject mainCamera;
	GameObject player;
	GameObject inventory;
	bool pickedUp = false;
	bool found = false;
	float FPS;
	public float pickUpDistance = 2f;
	bool lookIn = false;
	
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.deltaTime != 0)
			FPS = 1/Time.deltaTime;
		
		if(!found){
			inventory = GameObject.Find("Inventory(Clone)");
			if(inventory != null)
				found = true;
		}
		
		if(pickedUp){
			
			Vector3 pos = mainCamera.transform.position + mainCamera.transform.forward * 1.2f;
			pos.y -= 0.5f;
			rigidbody.velocity = (pos - transform.position) * FPS;//transform.position - pos;
			transform.rotation = player.transform.rotation;
			//rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		}
		
		else{
			rigidbody.useGravity = true;
		}
		
		if(Vector3.Distance(transform.position, player.transform.position) < pickUpDistance && (Input.GetButtonDown("Use") || Input.GetButtonDown("Action"))){
			if(!inventory.GetComponent<inventoryScript>().holdingObject && !pickedUp && lookIn){
				pickedUp = !pickedUp;
				inventory.GetComponent<inventoryScript>().holdingObject = true;
			}
			else if(pickedUp){
				inventory.GetComponent<inventoryScript>().holdingObject = false;
				pickedUp = !pickedUp;
			}
		}
			
	}
	void OnMouseDown(){
		if(Vector3.Distance(transform.position, player.transform.position) < pickUpDistance){
			if(!inventory.GetComponent<inventoryScript>().holdingObject && !pickedUp){
				pickedUp = !pickedUp;
				inventory.GetComponent<inventoryScript>().holdingObject = true;
			}
			else if(pickedUp){
				inventory.GetComponent<inventoryScript>().holdingObject = false;
				pickedUp = !pickedUp;
			}
		}
	}
	void OnMouseEnter(){
		lookIn = true;
	}
	void OnMouseExit(){
		lookIn = false;
	}
}
