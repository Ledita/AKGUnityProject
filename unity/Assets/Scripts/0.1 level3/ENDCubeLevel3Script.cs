using UnityEngine;
using System.Collections;

public class ENDCubeLevel3Script : MonoBehaviour {
	
	float timer = 0;
	bool trig = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(trig){
			timer += Time.deltaTime;
			if(timer > 0.3f)
				Application.LoadLevel(0);
		}
	}
	
	void OnTriggerEnter(Collider obj){							
		if(obj.gameObject.name == "Player")	trig = true;
	}
}
