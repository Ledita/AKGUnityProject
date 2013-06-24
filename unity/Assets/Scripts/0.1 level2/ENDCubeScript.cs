using UnityEngine;
using System.Collections;

public class ENDCubeScript : MonoBehaviour {
	
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
				Application.LoadLevel(3);
		}
	}
	
	void OnTriggerEnter(){							
		trig = true;
	}
}
