using UnityEngine;
using System.Collections;

public class passTroughtScript : MonoBehaviour {
	
	GameObject tutorial;
	bool done = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player" && !done){
			tutorial = GameObject.Find ("tutorialCapsule");
			tutorial.GetComponent<startSceenSript>().portableTerminalTutorialDone = false;
			done = true;
		}
	}
}
