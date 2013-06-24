using UnityEngine;
using System.Collections;

public class optionsControllerCheckboxScript : MonoBehaviour {
	
	public bool OnOff;
	public Texture2D on;
	public Texture2D off;
	GameObject options;
	
	
	// Use this for initialization
	void Start () {
		options = GameObject.Find("Options");
		OnOff = options.GetComponent<OptionsScript>().joystickOnOff;
	}
	
	// Update is called once per frame
	void Update () {
		if(OnOff){
			guiTexture.texture = on;
		}
		else{
			guiTexture.texture = off;
		}
	}
	void OnMouseDown(){
		if(OnOff) OnOff = false;
		else OnOff = true;
	}
}
