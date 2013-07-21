using UnityEngine;
using System.Collections;

public class flashLightScript : MonoBehaviour {
	
	public bool onOff = false;
	GameObject player;
	Vector3 playerPosPre;
	float playerSpeed;
	float dPadPre;
	float FPS;
	float lampShift;
	float lampShiftSpeed;
	float lampShiftDirection = 1f;
	public AudioClip[] flashOnSound;
	public AudioClip[] flashOffSound;
	float volume;
	public bool haveFlashlight = true;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		volume = player.GetComponent<playerControlScript>().volume;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.deltaTime != 0)
			FPS = 1/Time.deltaTime;
		
		if((Input.GetButtonDown("Flashlight") || (Input.GetAxis("Joystick Dpad Vertical") > 0 && Input.GetAxis("Joystick Dpad Vertical") != dPadPre)) && player.GetComponent<playerControlScript>().canMove && haveFlashlight){
			light.enabled = !light.enabled;
			onOff = light.enabled;
			volume = player.GetComponent<playerControlScript>().volume;
			if(onOff){
				AudioSource.PlayClipAtPoint(flashOnSound[ Random.Range(0, flashOnSound.Length-1) ], transform.position, volume);
			}
			else{
				AudioSource.PlayClipAtPoint(flashOffSound[ Random.Range(0, flashOffSound.Length-1) ], transform.position, volume);
			}
			
		}
		if(player.GetComponent<playerControlScript>().grounded){
			playerSpeed = player.GetComponent<playerControlScript>().playerSpeed;
//Debug.Log(playerSpeed);
			lampShiftSpeed = (playerSpeed + (playerSpeed * Random.value)) * lampShiftDirection * 5f / 90f;
			lampShift += lampShiftSpeed;// * Time.deltaTime;
//Debug.Log(lampShift);
			if(lampShift > 3f){
				lampShiftDirection = -1f;
			}
			else if(lampShift < -3f){
				lampShiftDirection = 1f;
			}
			lampShift = Mathf.Clamp(lampShift, -3f, 3f);
			transform.rotation = Quaternion.Euler(0, lampShift, 0) * Camera.mainCamera.transform.rotation;
			
			dPadPre = Input.GetAxis("Joystick Dpad Vertical");
			playerPosPre = player.transform.position;
		}
	}
}
