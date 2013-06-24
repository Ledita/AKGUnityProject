using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	
	GameObject player;
	bool ins = false;
	Vector3 prevPos;
	bool collided = false;


	// Use this for initialization
	void Start () {
		prevPos = transform.position;
		player = GameObject.Find("Player");
			
	}
	
	// Update is called once per frame
	void Update () {
		if(ins){
			player.GetComponent<playerControlScript>().enviromentalMovement = (transform.position - prevPos);
		}
		if(collided){
			rigidbody.constraints = RigidbodyConstraints.None;
			collided = false;
		}
	
		prevPos = transform.position;		
	}
	
	void InCollision(){
		rigidbody.isKinematic = true;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		collided = true;
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.name == "Player") ins = true;
Debug.Log("Platform IN");
	}
	
	void OnTriggerExit(Collider obj){
		if(obj.gameObject.name == "Player") ins = false;
		else if (obj.gameObject.name != gameObject.name) InCollision();
Debug.Log("Platform OUT");
	}
	
}
