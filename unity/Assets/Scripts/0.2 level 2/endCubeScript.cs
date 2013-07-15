using UnityEngine;
using System.Collections;

public class endCubeScript : MonoBehaviour {

	float timer = 0;
	bool trig = false;
	public int toLoadLevel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(trig){
			timer += Time.deltaTime;
			if(timer > 0.3f)
				Application.LoadLevel(toLoadLevel);
		}
		
	}
	
	void OnTriggerEnter(){
		trig = true;
		
	}
}
