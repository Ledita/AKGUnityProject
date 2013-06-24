using UnityEngine;
using System.Collections;

public class crouchBoxScript : MonoBehaviour {
	
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider inside){
		if(inside.gameObject.name == "Player"){
			player.GetComponent<playerControlScript>().canStandUp = false;
Debug.Log("IN");
		}
	}
	
	void OnTriggerExit(Collider inside) {
		if(inside.gameObject.name == "Player"){
			player.GetComponent<playerControlScript>().canStandUp = true;
Debug.Log("OUT");
		}
	}
}
