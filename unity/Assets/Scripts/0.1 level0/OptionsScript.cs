using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {
	
	public bool joystickOnOff = false;
	GameObject player;
	GameObject checkbox;
	public float controllerYSensitivity;
	public float mouseSensitivity;
	public float volume;
	
	// Use this for initialization
	void Start () {
		GameObject[] other = GameObject.FindGameObjectsWithTag("option");
		if(other.Length > 1) Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
		
		mouseSensitivity = 0.5f;
		controllerYSensitivity = 0.5f;
		volume = 0.5f;
		
		//set resolution
		Resolution[] resolutions = Screen.resolutions;
		Screen.SetResolution(resolutions[resolutions.Length-1].width, resolutions[resolutions.Length-1].height, true);
	}
	
	// Update is called once per frame
	void Update () {

		player = GameObject.Find("Player");
		checkbox = GameObject.Find ("controllerCheckbox");
		
		if(player != null){
			player.GetComponent<playerControlScript>().joystick = joystickOnOff;
			player.GetComponent<playerControlScript>().mouseSpeed = mouseSensitivity * 5f + 1f;
			player.GetComponent<playerControlScript>().joystickSpeed = controllerYSensitivity * 5f + 1f;
			player.GetComponent<playerControlScript>().volume = volume * 10f;
		}
		if(checkbox != null)
			joystickOnOff = checkbox.GetComponent<optionsControllerCheckboxScript>().OnOff;
//Debug.Log(volume);
//Debug.Log(joystickOnOff);
//Debug.Log (1/Time.deltaTime);
		
	}
}
