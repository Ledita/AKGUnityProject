using UnityEngine;
using System.Collections;

public class PlusButtonScript : MonoBehaviour {
	
	public Texture2D plusNormal;
	public Texture2D plusHover;
	GameObject terminal;
	public bool Pressed;
	
	void Start(){
		terminal = GameObject.Find("Terminal");
	}
	
	void OnMouseEnter(){
		guiTexture.texture = plusHover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = plusNormal;
	}
	
	void OnMouseDown(){
		terminal.GetComponent<terminalScript>().plusLine = true;
	}
}
