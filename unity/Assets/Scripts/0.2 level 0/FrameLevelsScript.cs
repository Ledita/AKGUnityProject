using UnityEngine;
using System.Collections;

public class FrameLevelsScript : MonoBehaviour {

	public Texture2D frameNormal;
	public Texture2D frameHover;
	public int levelNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseEnter(){
		guiTexture.texture = frameHover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = frameNormal;
	}
	
	void OnMouseDown(){
		Application.LoadLevel(levelNumber);
	}
}
