using UnityEngine;
using System.Collections;

public class doorScript : MonoBehaviour {
	
	bool opening;
	bool closing;
	Vector3 dir = new Vector3(0, -1f, 0);
	Vector3 startPos;

	
	
	
	// Use this for initialization
	void Start () {
		
Debug.Log(dir);
		startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if(opening){
			//Debug.Log("open");
			transform.position += dir * Time.deltaTime;
//Debug.Log(transform.position.y);
//Debug.Log(startPos.y);
			if(transform.position.y <= startPos.y - 3f){
				opening = false;
			}
		}
		if(closing){
			Debug.Log("close");
			transform.position -= dir * Time.deltaTime;
			if(transform.position.y >= startPos.y){
				closing = false;
			}
		}
	}
	
	public void Open () {
		opening = true;
		closing = false;
//Debug.Log("open");
	}
	
	public void Close () {
		closing = true;
		opening = false;
Debug.Log("close");
	}
}
