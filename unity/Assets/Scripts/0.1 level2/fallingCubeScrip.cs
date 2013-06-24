using UnityEngine;
using System.Collections;

public class fallingCubeScrip : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(){
		rigidbody.isKinematic = false;
		rigidbody.AddForce(0, 9.81f, 0);
		
	}
}
