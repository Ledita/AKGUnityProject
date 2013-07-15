using UnityEngine;
using System.Collections;

public class backPauseButtonScript : MonoBehaviour {

	public Texture2D Normal;
	public Texture2D Hover;
	GameObject pauseMenu;
	GameObject optionsButton;

	// Use this for initialization
	void Start () {
		pauseMenu = GameObject.Find("PauseMenu(Clone)");
		optionsButton = GameObject.Find("pauseOptions");
	}
	
		
	void OnMouseEnter(){
		guiTexture.texture = Hover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = Normal;
	}
	
	void OnMouseDown(){
		pauseMenu.transform.position = new Vector3(0, 0, 0);
		optionsButton.GetComponent<optionsPauseButtonScript>().optionsOn = false;
	}
}
