using UnityEngine;
using System.Collections;

public class portableTerminalScript : MonoBehaviour {
	
	public bool inSync = false;
	GameObject terminal;
	GameObject inventory;
	bool inventoryFound = false;
	public AudioClip syncSound;
	GameObject player;
	
	// Use this for initialization
	void Start () {
		terminal = GameObject.Find ("Terminal");
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(!inventoryFound){
			inventory = GameObject.Find("Inventory(Clone)");
			if(inventory != null)
				inventoryFound = true;
		}
		if(inventory.GetComponent<inventoryScript>().ringPos == 1 && (Input.GetButtonUp("Use") || Input.GetButtonUp("Action"))){
			if(inSync){
				terminal.GetComponent<terminalScript>().portableTerminal = true;
				terminal.GetComponent<terminalScript>().OnOff = true;
			}
			else{
				RaycastHit hit;
				if(Physics.Raycast(Camera.mainCamera.transform.position, Camera.mainCamera.transform.forward, out hit, 2f) && hit.collider.gameObject.name == "Terminal"){
					inSync = true;
					AudioSource.PlayClipAtPoint(syncSound, transform.position, player.GetComponent<playerControlScript>().volume);
				}
			}
		}
	
	}
}
