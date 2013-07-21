using UnityEngine;
using System.Collections;

public class inventoryScript : MonoBehaviour {
	
	GameObject invRing;
	GameObject invFlash;
	GameObject flashlight;
//	GameObject EMPTool;
	GameObject PortableTerminal;
	
	public bool haveFlashlight = true;
	public bool haveEMPTool = true;
	public bool havePortableTerminal = true;
	
	Vector3 invPos; //0.965, 0.95; 1.16, 1.28;
	public Texture2D flashOn;
	public Texture2D flashOff;
	public Texture2D[] ringState;
	public Texture2D holding;
	
	bool flashPre;
	bool bringUp = false;
	bool hide = false;
	bool flashTo;
	public int ringPos = 0; //0 empty, 2 portable terminal, 1 emp tool
	float hideTimer = 1;
	bool hideTimerCont = false;
	
	public AudioClip invSound;

	
	float Volume = 1f;
	float dpadPre;
	float mouseScrollPre;
	GameObject Player;
	float apearSpeed = 3.5f;
	public bool holdingObject = false;
	bool holdingObjectPre = false;
	int ringPosPre = 0;
	bool playSound = false;
	
	// Use this for initialization
	void Start () {
		invRing = GameObject.Find("invRing");
		invFlash = GameObject.Find("invFlashlight");
		flashlight = GameObject.Find("flashlight");
		PortableTerminal = GameObject.Find("PortableTerminal");
		Player = GameObject.Find("Player");
		invRing.guiTexture.pixelInset = new Rect (-Screen.width * 300f / 1920f / 2f, -Screen.height * 300f / 1080f / 2f, Screen.width * 300f / 1920f, Screen.height * 300f / 1080f);
		invFlash.guiTexture.pixelInset = new Rect (-Screen.width * 300f / 1920f / 2f, -Screen.height * 300f / 1080f / 2f, Screen.width * 300f / 1920f, Screen.height * 300f / 1080f);
		invPos = new Vector3(1.16f, 1.28f, 0);
		transform.position = invPos;
		
	}
	
	// Update is called once per frame
	void Update () {
		Volume = Player.GetComponent<playerControlScript>().volume;
//Debug.Log(Volume);
		if(Player.GetComponent<playerControlScript>().canMove){
			if(flashlight.GetComponent<flashLightScript>().onOff != flashPre){
				bringUp = true;
				flashTo = flashlight.GetComponent<flashLightScript>().onOff;
			}
			if(Input.GetButtonDown("Inventory 1")){	ringPos = 0; bringUp = true; }
			if(Input.GetButtonDown("Inventory 2")){	ringPos = 1; bringUp = true; }
			if(Input.GetButtonDown("Inventory 3")){	ringPos = 2; bringUp = true; }
			
			if(Input.GetAxis("Joystick Dpad Horizontal") > 0 && Input.GetAxis("Joystick Dpad Horizontal") != dpadPre ){ ringPos += 1; bringUp = true; }
			if(Input.GetAxis("Joystick Dpad Horizontal") < 0 && Input.GetAxis("Joystick Dpad Horizontal") != dpadPre ){ ringPos -= 1; bringUp = true; }
			
			if(Input.GetAxis("Mouse ScrollWheel") > 0 && Input.GetAxis("Mouse ScrollWheel") != mouseScrollPre ) {ringPos += 1; bringUp = true; }
			if(Input.GetAxis("Mouse ScrollWheel") < 0 && Input.GetAxis("Mouse ScrollWheel") != mouseScrollPre ) {ringPos -= 1; bringUp = true; }
			
			if(ringPos < 0) ringPos = 2;
			if(ringPos > 2) ringPos = 0;
			
			
			if(!havePortableTerminal && ringPos == 1){
				ringPos = 0;
			}
			if(!haveEMPTool && ringPos == 2){
				ringPos = 0;
			}
			
	
//Debug.Log(Input.GetAxis("Joystick Dpad Horizontal") + " " + ringPos);		
//Debug.Log(flashlight.GetComponent<flashLightScript>().onOff);
			
			if(hideTimerCont){
				hideTimer += Time.deltaTime;
			}
			if(ringPos != ringPosPre) playSound = true;
			
			if(bringUp){
				hide = false;
				invPos.x -= 0.195f * Time.deltaTime * apearSpeed;
				invPos.y -= 0.33f * Time.deltaTime * apearSpeed;
				if(invPos.x <= 0.965f && invPos.y <= 0.95f){
					bringUp = false;
					if(flashTo)
						invFlash.guiTexture.texture = flashOn;
					else
						invFlash.guiTexture.texture = flashOff;
					
					if(!holdingObject){
						invRing.guiTexture.texture = ringState[ringPos];
						if(playSound){
							AudioSource.PlayClipAtPoint(invSound, transform.position, Volume);
							playSound = false;
						}
					}
					else
						invRing.guiTexture.texture = holding;
					
					hideTimer = 0;
					hideTimerCont = true;
				}
			}
			if(holdingObject != holdingObjectPre){
				if(!holdingObject){
					ringPos = 0;
					invRing.guiTexture.texture = ringState[ringPos];
				}
				else
					invRing.guiTexture.texture = holding;
			}
			
			if(hideTimer > 0.7f && hideTimerCont){
				hideTimerCont = false;
				hide = true;
			}
			
			if(hide){
				bringUp = false;
				invPos.x += 0.195f * Time.deltaTime * apearSpeed;
				invPos.y += 0.33f * Time.deltaTime * apearSpeed;
				if(invPos.x >= 1.16f && invPos.y >= 1.28f)
					hide = false;
			}
			invPos.x = Mathf.Clamp(invPos.x, 0.965f, 1.16f);
			invPos.y = Mathf.Clamp(invPos.y, 0.95f, 1.28f);
			transform.position = invPos;
			
			if(ringPos == 1){
			//	Vector3 portableTerminalPos = (Player.transform.position + new Vector3 (0, 1.2f, 0.4f));
				PortableTerminal.transform.position = (Player.transform.rotation * new Vector3 (0, 1.2f, 0.4f)) + Player.transform.position;
				
				
				//Player.transform.rotation *
				PortableTerminal.renderer.enabled = true;
			}
			else{
				PortableTerminal.transform.position = Player.transform.rotation * new Vector3 (0, 0, -0.4f);
				PortableTerminal.renderer.enabled = false;
			}
			
			dpadPre = Input.GetAxis("Joystick Dpad Horizontal");
			mouseScrollPre = Input.GetAxis("Mouse ScrollWheel");
			flashPre = flashlight.GetComponent<flashLightScript>().onOff;
			
			holdingObjectPre = holdingObject;
			ringPosPre = ringPos;
		}
	}
}
