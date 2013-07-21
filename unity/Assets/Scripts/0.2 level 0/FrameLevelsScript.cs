using UnityEngine;
using System.Collections;

public class FrameLevelsScript : MonoBehaviour {

	public Texture2D frameNormal;
	public Texture2D frameHover;
	public int levelNumber;

	// Use this for initialization
	void Start () {
		guiTexture.pixelInset = new Rect(Screen.width * (600f / 1920f) / -2, Screen.height * (350 / 1080f) / -2, Screen.width * (600f / 1920f), Screen.height * (350 / 1080f));
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
