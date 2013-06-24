using UnityEngine;
using System.Collections;

public class CubeClick : MonoBehaviour {
	
	public Material texture1;
	public Material texture2;
	bool mat = false;
	bool ins = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(){	Debug.Log("Inside"); ins = true; }
	void OnTriggerExit(){ Debug.Log("Outside"); ins = false; }
	
	void OnMouseDown(){
		if(ins){
			if(mat){ renderer.material = texture1; mat = false; }
			else { renderer.material = texture2; mat = true; }
			Debug.Log("clicked");
		}
	}
}
