using UnityEngine;
using System.Collections;

public class part2resetScript : MonoBehaviour {
	
	public GameObject terminal;
	GameObject player;
	public GameObject cube;
	public GameObject platform;
	public GameObject portableTerminal;
	public GameObject middleTrigger;
	
	Vector3 platformPos;
	Vector3 cubePos;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		platformPos = platform.transform.position;
		cubePos = cube.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cheat")) Reset ();
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player") Reset();
	}
	
	void Reset(){
		player.transform.position = new Vector3(1f, 0.08f, -36f);
		player.GetComponent<playerControlScript>().verticalVelocity = 1f;
		player.GetComponent<playerControlScript>().horizontalSpeed = 0;
		player.GetComponent<playerControlScript>().forwardSpeed = 0;
		
		cube.transform.position = cubePos;
		platform.transform.position = platformPos;
		platform.GetComponent<platformScript>().ins = false;
		platform.GetComponent<platformScript>().Stop();
		platform.GetComponent<platformScript>().currentDirection = null;
		
		middleTrigger.GetComponent<levelPart2Script>().Part2();
		
		portableTerminal.GetComponent<portableTerminalScript>().inSync = false;
		terminal.GetComponent<terminalScript>().activeLine = 0;
		
		for(int i = 0; i < 8; i++){
			terminal.GetComponent<terminalScript>().lineText[i] = "";
			terminal.GetComponent<terminalScript>().errors[i] = false;
		}
	}
}