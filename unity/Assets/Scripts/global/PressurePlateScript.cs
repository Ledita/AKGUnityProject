using UnityEngine;
using System.Collections;

public class PressurePlateScript : MonoBehaviour {
	
	Vector3 posBasic;
	Vector3 pos;
	GameObject player;
	GameObject objectIn = null;
	
	public string objectName;
	public string objectDirection;
	bool objectOn = false;
	bool done = true;
	bool objOnPre = false;
	
	GameObject targetObject;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		posBasic = transform.position;
		pos = posBasic;
	}
	
	// Update is called once per frame
	void Update () {
		if(objectOn)
			pos.y = posBasic.y - 0.2f;
		else
			pos = posBasic;
		
		transform.position = pos;
	}
	
	public void AddObject(GameObject obj){
		Debug.Log ("Player In");
		if(obj.name == "Player" || obj.tag == "MovableObject"){
			if(objectIn == null){
				objectIn = obj;
				objectOn = true;
				Action();
			}
		}
	}
	
	public void RemoveObject(GameObject obj){
		if(obj.name == "Player" || obj.tag == "MovableObject"){
			if(objectIn == obj){
				objectIn = null;
				objectOn = false;
				End ();
			}
		}
	}
	
	void Action(){
		targetObject = GameObject.Find(objectName);

		if(objectName[0] == 'P'){	// platform
			
			targetObject.rigidbody.isKinematic = false;
			
			platformScript platScript = targetObject.GetComponent<platformScript>();
			platScript.movePlatform(objectDirection);
		}
		else if(objectName[0] == 'd'){ // door
			switch(objectDirection)
			{
			case "open" :
				//open
				break;
			case "close" :
				//close
				break;
			}
		}
	}
	
	void End(){
		targetObject = GameObject.Find(objectName);
		if(objectName[0] == 'P'){
			targetObject.GetComponent<platformScript>().Stop();
		}
	}
}
