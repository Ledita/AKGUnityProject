using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {
	
	public bool joystickOnOff = false;
	GameObject player;
	GameObject checkbox;
	public float controllerYSensitivity;
	public float mouseSensitivity;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		mouseSensitivity = 0.5f;
		controllerYSensitivity = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find("Player");
		checkbox = GameObject.Find ("controllerCheckbox");
		if(player != null){
			player.GetComponent<playerControlScript>().joystick = joystickOnOff;
			player.GetComponent<playerControlScript>().mouseSpeed = mouseSensitivity * 5f + 1f;
			player.GetComponent<playerControlScript>().joystickSpeed = controllerYSensitivity * 5f + 1f;
		}
		if(checkbox != null)
			joystickOnOff = checkbox.GetComponent<optionsControllerCheckboxScript>().OnOff;
//Debug.Log(joystickOnOff);
		
	}
}
