using UnityEngine;
using System.Collections;

public class FallingFloorScript : MonoBehaviour {
	
	float timer = 0;
	bool ins = false;
	float life;
	
	// Use this for initialization
	void Start () {
		life = Random.value * 0.3f + 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		if(ins){
			timer += Time.deltaTime;
			if(timer > life){
				rigidbody.isKinematic = false;
				rigidbody.AddForce(0, -9.81f, 0);
			}
		}
	}
	
	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.name == "Player" || obj.gameObject.tag == "FallingCube")
			ins = true;
	}
}
