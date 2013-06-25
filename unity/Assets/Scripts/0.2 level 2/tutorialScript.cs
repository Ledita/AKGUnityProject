using UnityEngine;
using System.Collections;

public class tutorialScript : MonoBehaviour {
	
	GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		player.GetComponent<playerControlScript>().canMove = false;
		player.GetComponent<pauseMenuScript>().canPause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			player.GetComponent<playerControlScript>().canMove = true;
			player.GetComponent<pauseMenuScript>().canPause = true;
			Application.LoadLevel(6);
			Screen.lockCursor = true;
		}
	}
}
