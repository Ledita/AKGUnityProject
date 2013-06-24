using UnityEngine;
using System.Collections;

public class exitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			Application.LoadLevel(1);
		}
	}	
}
