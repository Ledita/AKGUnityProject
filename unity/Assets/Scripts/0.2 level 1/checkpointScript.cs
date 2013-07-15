using UnityEngine;
using System.Collections;

public class checkpointScript : MonoBehaviour {
	
	public Vector3 putBack;
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.name == "Player"){
			player.transform.position = putBack;
			player.GetComponent<playerControlScript>().stamina = 3.5f;
			player.GetComponent<playerControlScript>().verticalVelocity = 0f;
		}
	}
}
