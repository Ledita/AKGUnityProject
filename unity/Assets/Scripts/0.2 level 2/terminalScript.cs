using UnityEngine;
using System.Collections;

public class terminalScript : MonoBehaviour {
	
	bool OnOff = false;
	bool OnOffCheck = true;
	public bool plusLine = false;
	GameObject terminal;
	GameObject player;
	GameObject MainCamera;
	GameObject aim;
	GameObject gui;
	public GameObject[] lines;
	string[] lineText = {"", "", "", "", "", "", "", ""};
	bool[] errors = {false, false, false, false, false, false, false, false};
	int activeLine = 0;
	Vector3 playerPosition;
	string textInput;
	string targetName;
	GameObject plusButton;
	
	// Use this for initialization
	void Start () {
		terminal = GameObject.Find("TerminalHolder");
		player = GameObject.Find("Player");
		MainCamera = GameObject.Find("Main Camera");
		aim = GameObject.Find("aim");
		gui = GameObject.Find("TerminalGui");
		//lines = GameObject.FindGameObjectsWithTag("terminalLine");
		plusButton = GameObject.Find("plusNormal");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			OnOff = false;
		}
		
		if(OnOff){
			player.GetComponent<playerControlScript>().canMove = false;
			player.GetComponent<pauseMenuScript>().canPause = false;
			playerPosition = new Vector3(terminal.transform.position.x, terminal.transform.position.y, terminal.transform.position.z - 1f);
			playerPosition = terminal.transform.rotation * playerPosition;
			player.transform.position = playerPosition;
			player.transform.rotation = terminal.transform.rotation;
			MainCamera.transform.localRotation = Quaternion.Euler(33.5f, 0, 0);
			MainCamera.camera.fieldOfView = 35f;
			aim.transform.position = new Vector3(1.5f, 0.5f, 0);
			Screen.lockCursor = false;
			gui.transform.position = new Vector3(0,0,0);
			
			
			
			for(int i = 0; i <= lines.Length - 1; i++){
				if((lineText[i] == " " || lineText[i] == null || lineText[i].Length <= 1f) && i < activeLine - 1f && i != lines.Length - 1){
					lineText[i] = lineText[i+1];
					lineText[i+1] = null;
				}
				
				lines[i].guiText.text = lineText[i];
				if(errors[i])
					lines[i].guiText.text = lineText[i] + "   !error!";
				
				if(lines[i].GetComponent<lineClickScript>().pressed == true){
					lines[i].GetComponent<lineClickScript>().pressed = false;
					activeLine = i;
				}
			}
			
			lineText[activeLine] = TextIn(lineText[activeLine]);
			lines[activeLine].guiText.text = lineText[activeLine] + "_";
			
			if((plusLine || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)) && activeLine != 7){
			/*	while(lineText[activeLine][0] == ' '){
					lineText[activeLine] = lineText[activeLine].Remove(0, 1);
				}*/
				errors[activeLine] = Process(lineText[activeLine]);
Debug.Log(activeLine);
				activeLine++;
Debug.Log(activeLine);
				plusLine = false;

			}
			if(Input.GetKeyDown(KeyCode.UpArrow) && activeLine != 0)
				activeLine--;
			
			
			
		}
		else if (OnOff != OnOffCheck && !OnOff){
			MainCamera.camera.fieldOfView = 60f;
			player.GetComponent<playerControlScript>().canMove = true;
			aim.transform.position = new Vector3(0.5f, 0.5f, 0);
			Screen.lockCursor = true;
			gui.transform.position = new Vector3(1f,0,0);
		}
		else
			player.GetComponent<pauseMenuScript>().canPause = true;
		
		OnOffCheck = OnOff;
	}
	
	void OnMouseDown()
	{
		if(!player.GetComponent<pauseMenuScript>().paused) OnOff = true;
	}
	
	string TextIn(string text){
		string inC = Input.inputString;
		if(inC == "\b")	text = text.Substring(0, text.Length - 1);
		else if (inC == "\n" || inC == "\r") text += " ";
		else text += inC;
		return text;
	}
	
	bool Process(string text){
		bool error = false;
		string[] words;
		string[] task = {"", ""};
		GameObject targetObject;
		
		if(lineText[activeLine][0] == ' '){
			lineText[activeLine] = lineText[activeLine].Remove(0, 1);
		}
		
		words = text.Split(' ');
		if(words[0] == "if"){
			targetName = words[1];
		}
		else error = true;
		
		
		if(words[2] == "then"){
			task = words[3].Split('.');
		}
		else error = true;	
		

		targetObject = GameObject.Find(targetName); //finding the button
		if(targetObject == null)
			error = true;
		else {
			targetObject.GetComponent<globalButtonScrip>().objectName = task[0];
			targetObject.GetComponent<globalButtonScrip>().objectDirection = task[1];
		}
		return error;
	}
}
