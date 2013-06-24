using UnityEngine;
using System.Collections;

public class fallingFloorScript : MonoBehaviour {
	
	float timer = 0;
	float life;
	
	bool ins = false;
	// Use this for initialization
	void Start () {
		life = Random.value * 0.3f + 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		if(ins){
			timer += Time.deltaTime;
			if(timer > life)
			{
				rigidbody.isKinematic = false;
				rigidbody.AddForce(0, -9.81f, 0);
			}
		}
	}

	void OnTriggerEnter(){
		ins = true;
	}
}
