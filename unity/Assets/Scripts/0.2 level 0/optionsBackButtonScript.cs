using UnityEngine;
using System.Collections;

public class optionsBackButtonScript : MonoBehaviour {

	public Texture2D StartNormal;
	public Texture2D StartHover;
	GameObject screen;
	public GameObject options;

	// Use this for initialization
	void Start () {
		screen = GameObject.Find("GuiObjects");
	}
	
		
	void OnMouseEnter(){
		guiTexture.texture = StartHover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = StartNormal;
	}
	
	void OnMouseDown(){
		options.GetComponent<optionsButtonScript>().optionsOn = false;
		screen.transform.position = new Vector3(0, 0, 0);
	}
}
