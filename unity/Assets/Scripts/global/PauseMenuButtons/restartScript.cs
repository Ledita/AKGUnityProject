using UnityEngine;
using System.Collections;

public class restartScript : MonoBehaviour {
	
	public Texture2D Normal;
	public Texture2D Hover;
	
	void OnMouseEnter(){
		guiTexture.texture = Hover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = Normal;
	}
	
	void OnMouseDown(){
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}
}
