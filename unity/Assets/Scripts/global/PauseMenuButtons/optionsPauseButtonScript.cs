using UnityEngine;
using System.Collections;

public class optionsPauseButtonScript : MonoBehaviour {

	public Texture2D Normal;
	public Texture2D Hover;
	GameObject options;
	public bool optionsOn;
	
	void Start(){
		options = GameObject.Find("PauseMenu(Clone)");	
	}
	
	void OnMouseEnter(){
		guiTexture.texture = Hover;
	}
	
	void OnMouseExit(){
		guiTexture.texture = Normal;
	}
	
	void OnMouseDown(){
		options.transform.position = new Vector3 (1f, 0,0);
		optionsOn = true;
	}
}
