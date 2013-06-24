using UnityEngine;
using System.Collections;

public class endFallingCubesScript : MonoBehaviour {
	
	GameObject fallout;
	public bool done = false;
	
	// Use this for initialization
	void Start () {
		fallout = GameObject.Find("fallout");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(){
		if(!done){
			fallout.GetComponent<Part5Script>().end();
			done = true;
		}
	}
}
