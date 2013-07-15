using UnityEngine;
using System.Collections;

public class ResumeScript : MonoBehaviour {

	public Texture2D Hover;
	public Texture2D Normal;
	GameObject Player;
	
	void Start(){
		Player = GameObject.Find("Player");
	}
	
	void OnMouseEnter(){
		guiTexture.texture = Hover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = Normal;
	}
	
	void OnMouseDown(){
		Player.GetComponent<pauseMenuScript>().swichPause = true;
	}
}
