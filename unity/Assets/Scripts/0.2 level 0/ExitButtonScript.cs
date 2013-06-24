using UnityEngine;
using System.Collections;

public class ExitButtonScript : MonoBehaviour {
	
	public Texture2D exitNormal;
	public Texture2D exitHover;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseEnter(){
		guiTexture.texture = exitHover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = exitNormal;
	}
	
	void OnMouseDown(){
		Application.Quit();
	}
}
