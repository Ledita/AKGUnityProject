using UnityEngine;
using System.Collections;

public class backButtonScript : MonoBehaviour {

	public Texture2D StartNormal;
	public Texture2D StartHover;
	GameObject screen;

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
		
		screen.transform.position = new Vector3(0, 0, 0);
	}
}
