using UnityEngine;
using System.Collections;

public class flashLightScript : MonoBehaviour {
	
	public bool onOff = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Flashlight"))
			light.enabled = !light.enabled;
				
			
	}
}
