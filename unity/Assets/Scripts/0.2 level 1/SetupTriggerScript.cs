using UnityEngine;
using System.Collections;

public class SetupTriggerScript : MonoBehaviour {
	
	GameObject fallout;
	
	// Use this for initialization
	void Start () {
		fallout = GameObject.Find("fallout");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(){
		fallout.GetComponent<Part5Script>().setup();
	}
}
