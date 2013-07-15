using UnityEngine;
using System.Collections;

public class trasformForwardScript : MonoBehaviour {
	
	GameObject mainCamera;
	GameObject player;
	bool pickedUp = false;
	
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(pickedUp){
			Vector3 pos = mainCamera.transform.position + mainCamera.transform.forward * 1.2f;
			pos.y -= 0.5f;
			transform.position = pos;
			transform.rotation = player.transform.rotation;
			rigidbody.isKinematic = true;
		}
		else{
			rigidbody.isKinematic = false;
		}
			
	}
	void OnMouseDown(){
		pickedUp = !pickedUp;	
	}
}
