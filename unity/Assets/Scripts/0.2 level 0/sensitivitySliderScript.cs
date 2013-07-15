using UnityEngine;
using System.Collections;

public class sensitivitySliderScript : MonoBehaviour {
	
	public GameObject slideBar;
	public GameObject text;
	public GameObject checkBox;
	public GameObject optionsObject;
	public GameObject optionsButton;
	bool pressed = false;
	float slide;	//in pixels
	float sensitivity = 0.5f;	// in percent
	float timer = 0;
	
	// Use this for initialization
	void Start () {
		slideBar.guiTexture.pixelInset = new Rect(-Screen.width / 4f, slideBar.guiTexture.pixelInset.y, Screen.width / 2f, slideBar.guiTexture.pixelInset.height);
		optionsObject = GameObject.Find("Options");
	}
	
	// Update is called once per frame
	void Update () {
		optionsObject = GameObject.Find("Options");
		if(pressed){
			
			slide = Input.mousePosition.x;
			slide = Mathf.Clamp(slide, Screen.width * 0.4f, Screen.width * 0.9f);

//Debug.Log(sensitivity.ToString());
Debug.Log(checkBox.GetComponent<optionsControllerCheckboxScript>().OnOff);
			
			if(checkBox.GetComponent<optionsControllerCheckboxScript>().OnOff){
				text.guiText.text = "Controller sensitivity:";
				
				sensitivity = optionsObject.GetComponent<OptionsScript>().controllerYSensitivity;
				transform.position = new Vector3(sensitivity / 2f + 0.4f, transform.position.y, transform.position.z);
				sensitivity = slide / Screen.width * 2f - 0.8f;
				sensitivity = Mathf.Clamp(sensitivity, 0, 1);
				optionsObject.GetComponent<OptionsScript>().controllerYSensitivity = sensitivity;
			}
			else{
				text.guiText.text = "Mouse sensitivity:";
				
				sensitivity = optionsObject.GetComponent<OptionsScript>().mouseSensitivity;
				transform.position = new Vector3(sensitivity / 2f + 0.4f, transform.position.y, transform.position.z);
				sensitivity = slide / Screen.width * 2f - 0.8f;
				sensitivity = Mathf.Clamp(sensitivity, 0, 1);
				optionsObject.GetComponent<OptionsScript>().mouseSensitivity = sensitivity;
			}
			
			
			timer += Time.deltaTime;

//Debug.Log (timer);
		}
		if(Application.loadedLevel == 0){
			if(optionsButton.GetComponent<optionsButtonScript>().optionsOn){
				if(checkBox.GetComponent<optionsControllerCheckboxScript>().OnOff){
					text.guiText.text = "Controller sensitivity:";
					sensitivity = optionsObject.GetComponent<OptionsScript>().controllerYSensitivity;
					transform.position = new Vector3(sensitivity / 2f + 0.4f, transform.position.y, transform.position.z);
				}
				else{
					text.guiText.text = "Mouse sensitivity:";
					sensitivity = optionsObject.GetComponent<OptionsScript>().mouseSensitivity ;
	//Debug.Log(sensitivity);
					transform.position = new Vector3(sensitivity / 2f + 0.4f, transform.position.y, transform.position.z);
				}
			}
		}
		else{
			if(optionsButton.GetComponent<optionsPauseButtonScript>().optionsOn){
				if(checkBox.GetComponent<optionsControllerCheckboxScript>().OnOff){
					text.guiText.text = "Controller sensitivity:";
					sensitivity = optionsObject.GetComponent<OptionsScript>().controllerYSensitivity;
					transform.position = new Vector3(sensitivity / 2f + 0.4f, transform.position.y, transform.position.z);
				}
				else{
					text.guiText.text = "Mouse sensitivity:";
					sensitivity = optionsObject.GetComponent<OptionsScript>().mouseSensitivity ;
	//Debug.Log(sensitivity);
					transform.position = new Vector3(sensitivity / 2f + 0.4f, transform.position.y, transform.position.z);
				}
			}
			else{
				transform.position = new Vector3(sensitivity / 2f + 1.4f, transform.position.y, transform.position.z);
			}
//Debug.Log(optionsButton.GetComponent<optionsPauseButtonScript>().optionsOn);
		}
		
	}
	
	void OnMouseDown(){
		if(pressed) pressed = false;
		else pressed = true;
		timer = 0;
	}
	void OnMouseUp(){
		if(pressed && timer > 0.2f) pressed = false;
	}

}
