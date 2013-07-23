using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	
	GameObject player;
	public bool ins = false;
	bool isPlayer = false;
	GameObject ObjectOn;
	Vector3 prevPos;
	bool collided = false;
	public string currentDirection = null;
	float playerUp = 0f;
	float FPS;
	Vector3 velocity;


	// Use this for initialization
	void Start () {
		prevPos = transform.position;
		player = GameObject.Find("Player");
			
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(0,0,0);
		if(Time.deltaTime != 0)
			FPS = 1/Time.deltaTime;
		if(ins){
			if(isPlayer)
				player.GetComponent<playerControlScript>().enviromentalMovement = (transform.position - prevPos);
			else{}
				//ObjectOn.rigidbody.velocity = (transform.position - prevPos) * FPS * 1.042f;
//Debug.Log(FPS);

			}
		if(collided && !ins){
			rigidbody.constraints = RigidbodyConstraints.None;
			collided = false;
//Debug.Log("collided");
		}
	
		prevPos = transform.position;		
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name != "hitbox" && col.gameObject.tag == "MovableObject" && col.gameObject.tag != "Player"){
			Stop ();
Debug.Log ("collision");
		}
		
	}

	void OnTriggerEnter(Collider obj){
		Debug.Log(obj.gameObject.tag);
		if(obj.gameObject.tag == "Player"){ ins = true; isPlayer = true;}
		else if(obj.gameObject.name != "hitbox"){
			Debug.Log ("trigger");
			Stop();
//Debug.Log("colllide");
		}
	}
	
	void OnTriggerExit(Collider obj){
		if(obj.gameObject.name == "Player"){ ins = false; isPlayer = false; }
		//else if (obj.gameObject.name != gameObject.name) InCollision();
//Debug.Log("Platform OUT");
	}
	
	public void Stop(){
		if(!isPlayer){
			rigidbody.isKinematic = true;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			collided = true;
			currentDirection = null;
//Debug.Log ("stop");
		}
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
					rigidbody.AddForce(0, 0, 200f);
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
