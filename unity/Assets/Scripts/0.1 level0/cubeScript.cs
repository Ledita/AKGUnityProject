using UnityEngine;
using System.Collections;

public class cubeScript : MonoBehaviour {
	
	//float maxLife;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisonEnter(Collision col){
		if(col.gameObject.name == "Plane"){
			gameObject.isStatic = true;
		}
	}
}
