using UnityEngine;
using System.Collections;

public class titleSizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		guiTexture.pixelInset = new Rect (Screen.width * 1024f / 1920f / -2f, Screen.height * 512f / 1080f / -2f, Screen.width * 1024f / 1920f, Screen.height * 512f / 1080f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
