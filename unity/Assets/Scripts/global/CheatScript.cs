using UnityEngine;
using System.Collections;

public class CheatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cheat")) Application.LoadLevel( Application.loadedLevel + 1 );
	}
}
