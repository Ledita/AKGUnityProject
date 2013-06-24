using UnityEngine;
using System.Collections;

public class FallOutScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider fallen){
		if(fallen.gameObject.name == "Player") Application.LoadLevel(Application.loadedLevel);
	}
}
