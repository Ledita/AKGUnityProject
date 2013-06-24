using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
	
	GameObject player;
	bool ins = false;
	Vector3 prevPos;


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
	
		prevPos = transform.position;		
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.name != "Player")
			rigidbody.isKinematic = true;
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.name == "Player") ins = true;
Debug.Log("Platform IN");
	}
	
	void OnTriggerExit(Collider obj){
		if(obj.gameObject.name == "Player") ins = false;
Debug.Log("Platform OUT");
	}
	
}
