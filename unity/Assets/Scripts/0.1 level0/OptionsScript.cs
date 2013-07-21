using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour {
	
	GameObject player;
	GameObject checkbox;
	GameObject flashLight;
	GameObject inventory;
	
	public float controllerYSensitivity;
	public float mouseSensitivity;
	public float volume;
	
	public bool haveFlashlight = true;
	public bool havePortableTerminal = false;
	public bool haveEMPTool = false;
	public bool joystickOnOff = false;
	
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
		flashLight = GameObject.Find ("flashlight");
		inventory = GameObject.Find ("Inventory(Clone)");
		
		if(player != null){
			player.GetComponent<playerControlScript>().joystick = joystickOnOff;
			player.GetComponent<playerControlScript>().mouseSpeed = mouseSensitivity * 5f + 1f;
			player.GetComponent<playerControlScript>().joystickSpeed = controllerYSensitivity * 5f + 1f;
			player.GetComponent<playerControlScript>().volume = volume * 10f;
		}
		if(checkbox != null)
			joystickOnOff = checkbox.GetComponent<optionsControllerCheckboxScript>().OnOff;
		if(flashLight != null)
			flashLight.GetComponent<flashLightScript>().haveFlashlight = haveFlashlight;
		if(inventory != null){
			inventory.GetComponent<inventoryScript>().haveEMPTool = haveEMPTool;
			inventory.GetComponent<inventoryScript>().haveFlashlight = haveFlashlight;
			inventory.GetComponent<inventoryScript>().havePortableTerminal = havePortableTerminal;
		}
		if(Application.loadedLevel == 0){
			havePortableTerminal = false;
			haveEMPTool = false;
		}
		
//Debug.Log(volume);
//Debug.Log(joystickOnOff);
//Debug.Log (1/Time.deltaTime);
		
	}
}
