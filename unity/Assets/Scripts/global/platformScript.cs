using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	
	GameObject player;
	bool ins = false;
	Vector3 prevPos;
	bool collided = false;
	string currentDirection = null;
	float playerUp = 0f;


	// Use this for initialization
	void Start () {
		prevPos = transform.position;
		player = GameObject.Find("Player");
			
	}
	
	// Update is called once per frame
	void Update () {
		if(ins){
			player.GetComponent<playerControlScript>().enviromentalMovement = (transform.position - prevPos);
			//player.GetComponent<playerControlScript>().enviromentalMovement += new Vector3(0, playerUp, 0);
		}
		if(collided){
			rigidbody.constraints = RigidbodyConstraints.None;
			collided = false;
		}
	
		prevPos = transform.position;		
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name != "hitbox"){
			rigidbody.isKinematic = true;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			collided = true;
			currentDirection = null;
		}
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.name == "Player") ins = true;
//Debug.Log("Platform IN");
	}
	
	void OnTriggerExit(Collider obj){
		if(obj.gameObject.name == "Player") ins = false;
		//else if (obj.gameObject.name != gameObject.name) InCollision();
Debug.Log("Platform OUT");
	}
	
	public void movePlatform(string direction){
		if(direction != currentDirection){
			
			
			switch(direction)
			{
			case "north" : // x+
				rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				if(currentDirection == "south")
					rigidbody.AddForce(200f, 0, 0);
				else
					rigidbody.AddForce(100f, 0, 0);
				break;
			case "south" : // x-
				rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				if(currentDirection == "north")
					rigidbody.AddForce(-200f, 0, 0);
				else
					rigidbody.AddForce(-100f, 0, 0);
				break;
			case "down" : // y-
				rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				if(currentDirection == "up")
					rigidbody.AddForce(0, -200f, 0);
				else
					rigidbody.AddForce(0, -100f, 0);
				break;
			case "up" : // y+
				rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
				if(currentDirection == "down")
					rigidbody.AddForce(0, 200f, 0);
				else
					rigidbody.AddForce(0, 100f, 0);
				break;
			case "east" : // z-
				rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
				if(currentDirection == "west")
					rigidbody.AddForce(0, 0, -200f);
				else
					rigidbody.AddForce(0, 0, -100f);
				break;
			case "west" : // z+
				rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
				if(currentDirection == "east")
					rigidbody.AddForce(0, 0, 200f);
				else
					rigidbody.AddForce(0, 0, 100f);
				break;
			}
			
			if(direction == "up")
				playerUp = 0.2f;
			else
				playerUp = 0f;
			currentDirection = direction;
		}
	}
}
