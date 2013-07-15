using UnityEngine;
using System.Collections;

public class rigidbodyVelocityScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision obj){
		rigidbody.velocity = new Vector3 (0, 0, -10f);
	}
}
