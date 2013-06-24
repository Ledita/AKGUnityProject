using UnityEngine;
using System.Collections;

public class lineClickScript : MonoBehaviour {
	 public bool pressed = false;
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){
		pressed = true;	
	}
}
