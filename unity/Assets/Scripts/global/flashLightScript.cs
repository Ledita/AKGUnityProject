using UnityEngine;
using System.Collections;

public class flashLightScript : MonoBehaviour {
	
	public bool onOff = false;
	GameObject player;
	float dPadPre;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetButtonDown("Flashlight") || (Input.GetAxis("Joystick Dpad Vertical") > 0 && Input.GetAxis("Joystick Dpad Vertical") != dPadPre)) && player.GetComponent<playerControlScript>().canMove){
			light.enabled = !light.enabled;
			onOff = light.enabled;
		}
		dPadPre = Input.GetAxis("Joystick Dpad Vertical");
	}
}
