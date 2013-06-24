using UnityEngine;
using System.Collections;

public class NotesScript : MonoBehaviour {
	
	
	//GameObject platform
	
	// Use this for initialization
	void Start () {
		//platform = GameObject.Find("name");			// find object called name
	}
	
	// Update is called once per frame
	void Update () {
		//platform.GetComponent<platformScript>().hit;	//Get public variable from other script
		
	}
	/*
	void OnTriggerEnter(){								//belépés egy érzékelőbe
		insi = true;
	}
	void OnTriggerExit(){								//kilépés egy érzékelőből
		insi = false;
	}
	
	void OnMouseDown(){									//rákattítva valamire
		if(insi){
		
		}
	}
	*/	
	
}
