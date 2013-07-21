using UnityEngine;
using System.Collections;

public class startSceenSript : MonoBehaviour {
	
	public GameObject guiPrefab;
	GameObject guiObject;
	GameObject options;
	Object[] startObjects;
	
	bool flashTutorialDone = false;
	bool pressureTutorialDone = true;
	bool cubeTutorialDone = true;
	public bool portableTerminalTutorialDone = true;
	int portableTerminalTutorialPhase = 0;
	bool enterd = false;
	bool exited = false;
	
	// Use this for initialization
	void Start () {
		startObjects = GameObject.FindObjectsOfType(typeof(startSceenSript));
Debug.Log(startObjects.Length);
		if(startObjects.Length > 1){
			Destroy(gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
		Instantiate(guiPrefab, new Vector3(0.5f, 0.12f, 1f), Quaternion.identity);
		guiObject = GameObject.Find("GUI Text(Clone)");
		options = GameObject.Find("Options");
		guiObject.guiText.text = null;

		if(options != null){
			options.GetComponent<OptionsScript>().haveFlashlight = true;
			options.GetComponent<OptionsScript>().haveEMPTool = false;
			options.GetComponent<OptionsScript>().havePortableTerminal = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		guiObject = GameObject.Find("GUI Text(Clone)");
		if(Application.loadedLevel != 7)
			Destroy(gameObject);
		if(!flashTutorialDone){
			guiObject.guiText.text = "press E to turn the flashlight on";
			if(Input.GetKeyDown(KeyCode.E)){
				guiObject.guiText.text = null;
				flashTutorialDone = true;
			}
		}
		if(!pressureTutorialDone){
			guiObject.guiText.text = "use the if PressureplateN then command to use the Pressureplate";
			if(Input.GetMouseButtonDown(0)){
				guiObject.guiText.text = null;
				pressureTutorialDone = true;
			}
		}
		if(!cubeTutorialDone){
			guiObject.guiText.text = "press F on the cube to pick it up, if it's near";
			if(Input.GetKeyUp(KeyCode.F)){
				guiObject.guiText.text = null;
				cubeTutorialDone = true;
			}
		}
		if(!portableTerminalTutorialDone){
			if(portableTerminalTutorialPhase == 0){
				guiObject.guiText.text = "press 2 to equip the portable terminal";
				if(Input.GetKeyDown(KeyCode.Alpha2)) portableTerminalTutorialPhase++;
			}
			else if(portableTerminalTutorialPhase == 1){
				guiObject.guiText.text = "press F on the terminals screen to syncronise with it";
				if(Input.GetButtonDown("Use")) portableTerminalTutorialPhase++;
			}
			else if(portableTerminalTutorialPhase == 2){
				guiObject.guiText.text = "press F again to use the portable terminal";
				if(Input.GetButtonDown("Use")){
					guiObject.guiText.text = null;
					portableTerminalTutorialDone = true;
				}
			}
		}
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Player" && !enterd){
			pressureTutorialDone = false;
			enterd = true;
		}
	}
	void OnTriggerExit(Collider col){
		if(col.gameObject.name == "Player" && !exited){
			cubeTutorialDone = false;
			exited = true;
		}
	}
}
