using UnityEngine;
using System.Collections;

public class inventoryScript : MonoBehaviour {
	
	GameObject invRing;
	GameObject invFlash;
	GameObject flashlight;
//	GameObject EMPTool;
//	GameObject PortableTerminal;
	Vector3 invPos; //0.965, 0.95; 1.16, 1.28;
	public Texture2D flashOn;
	public Texture2D flashOff;
	public Texture2D[] ringState;
	bool flashPre;
	bool bringUp = false;
	bool hide = false;
	bool flashTo;
	int ringPos = 0; //0 empty, 1 portable terminal, 2 emp tool
	float hideTimer = 1;
	bool hideTimerCont = false;
	public AudioClip invSound;
	float Volume = 1f;
	float dpadPre;
	float mouseScrollPre;
	GameObject Player;
	float apearSpeed = 3.5f;
	
	// Use this for initialization
	void Start () {
		invRing = GameObject.Find("invRing");
		invFlash = GameObject.Find("invFlashlight");
		flashlight = GameObject.Find("flashlight");
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
	
//Debug.Log(Input.GetAxis("Joystick Dpad Horizontal") + " " + ringPos);		
//Debug.Log(flashlight.GetComponent<flashLightScript>().onOff);
			
			if(hideTimerCont){
				hideTimer += Time.deltaTime;
			}
			
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
					invRing.guiTexture.texture = ringState[ringPos];
					
					AudioSource.PlayClipAtPoint(invSound, transform.position, Volume);
					
					hideTimer = 0;
					hideTimerCont = true;
				}
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
			
			dpadPre = Input.GetAxis("Joystick Dpad Horizontal");
			mouseScrollPre = Input.GetAxis("Mouse ScrollWheel");
			flashPre = flashlight.GetComponent<flashLightScript>().onOff;
		}
	}
}
