using UnityEngine;
using System.Collections;

public class optionsButtonScript : MonoBehaviour {

	public Texture2D optionsNormal;
	public Texture2D optionsHover;
	GameObject screen;
	public bool optionsOn = false;

	// Use this for initialization
	void Start () {
		screen = GameObject.Find("GuiObjects");
	}
	
		
	void OnMouseEnter(){
		guiTexture.texture = optionsHover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = optionsNormal;
	}
	
	void OnMouseDown(){
		screen.transform.position = new Vector3(1f, 0, 0);
		optionsOn = true;
Debug.Log (optionsOn);
	}
}
