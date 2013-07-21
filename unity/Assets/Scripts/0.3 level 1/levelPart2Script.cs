using UnityEngine;
using System.Collections;

public class levelPart2Script : MonoBehaviour {
	
	GameObject options;
	GameObject light;
	GameObject terminalHolder;
	
	// Use this for initialization
	void Start () {
		options = GameObject.Find("Options");
		light = GameObject.Find("Main Light");
		terminalHolder = GameObject.Find("TerminalHolder");
//must be removed

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Player"){
			Part2();
		}
	}
	public void Part2 (){
		light.light.enabled = true;
		terminalHolder.transform.position = new Vector3(12f, -2f, -35.5f);
		options.GetComponent<OptionsScript>().havePortableTerminal = true;
	}
}
