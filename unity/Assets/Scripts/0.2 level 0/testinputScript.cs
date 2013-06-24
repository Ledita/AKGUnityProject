using UnityEngine;
using System.Collections;

public class testinputScript : MonoBehaviour {
	
	string text;
	string inC;
	string LastWordString;
	public GUIText outputScreen;
	string output;
	string[] words;
	bool inputing = true;
	bool process = false;
	string target;
	string[] task;
	GameObject targetObject;
	
    void Update() {
		if(inputing){
		
			inC = Input.inputString;
			output = null;
			
			if(inC == "\b")	text = text.Substring(0, text.Length - 1);
			else if (inC == "\n" || inC == "\r") process = true;
			else text += inC;

			
			guiText.text = text;
			//Debug.Log (text);
		}
		if(process){
			words = text.Split(' ');
			if(words[0] == "if"){
				output += words[1];
				target = words[1];
			}
			else output = "error";
			if(words[2] == "then"){
				output += words[3];
				task = words[3].Split('.');
			}
			else output += "error";	
			

			targetObject = GameObject.Find(target);
			if(targetObject == null)
				output += "input target not found: " + target;
			else targetObject.guiText.text = task[0];
			outputScreen.guiText.text = output;
			foreach(string word in task)
			{
				Debug.Log(word);
			}
			process = false;			
				
		}
			
	}
}
        

