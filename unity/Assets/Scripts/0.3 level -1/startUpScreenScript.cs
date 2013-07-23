using UnityEngine;
using System.Collections;

public class startUpScreenScript : MonoBehaviour {
	
	public GameObject startUpUnity;
	public GameObject startUpTitle;
	GameObject options;
	public AudioClip starupSound;
	
	bool played = false;
	
	float unityAlpha = 0f;
	float titleAlpha = 0f;
	
	float fadeSpeed = 2f;
	float fadeTimer;
	
	bool startTimer = true;
	float startTimerTimer = 0;
	
	bool middleWait = false;
	float middleTimer = 0;
	
	bool unityTexture = false;
	bool unityFadeIn = true;
	bool unityFadeWait = false;
	float unityFadeTimer = 0;
	
	bool titleTexture = false;
	bool titleFadeIn = false;
	bool titleFadeWait = false;
	float titleFadeTimer = 0;
	bool titleMove = false;
	
	Vector3 titleMoveTo = new Vector3(0.5f, 0.8f, 1f);
	Vector3 titleStartPos;
	Vector2 titleSizeTo = new Vector2(1024f, 512f);
	Vector2 titleStartSize = new Vector2(1800f, 900f);
	Vector2 titleSize;
	float titleSizeTimer = 0;
	bool end;
	
	// Use this for initialization
	void Start () {
		
		startUpUnity.guiTexture.pixelInset = new Rect ( Screen.width / -2f, Screen.height / -2, Screen.width, Screen.height);
		startUpTitle.guiTexture.pixelInset = new Rect ( Screen.width * 1800f / 1920f / -2f, Screen.height * 900f / 1080f / -2, Screen.width * 1800f / 1920f, Screen.height * 900f / 1080f);
		
		startUpUnity.guiTexture.color = new Color ( 0.5f, 0.5f, 0.5f, unityAlpha);
		startUpTitle.guiTexture.color = new Color ( 0.5f, 0.5f, 0.5f, titleAlpha);
		titleStartPos = startUpTitle.transform.position;
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("JoystickPause") || Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)){
			end = true;
		}
		
		if(startTimer){
			startTimerTimer += Time.deltaTime;
			startUpUnity.guiTexture.color = new Color ( 0.5f, 0.5f, 0.5f, unityAlpha);
			startUpTitle.guiTexture.color = new Color ( 0.5f, 0.5f, 0.5f, titleAlpha);
			if(startTimerTimer > 1f){
				if(!played){
					played = true;
					AudioSource.PlayClipAtPoint(starupSound, transform.position, 1f);
				}
			}
			if (startTimerTimer > 1.48f){
				startTimer = false;
				unityTexture = true;
			}
		}
		else if(unityTexture){
			if(unityFadeIn){
				if(!unityFadeWait){
					unityAlpha = alphaFuncion();
	        		startUpUnity.guiTexture.color = new Color(1f, 1f, 1f,unityAlpha);
	        		if(unityAlpha > 0.5f){ unityFadeWait = true; fadeTimer = 0; }
				}
				else {
					unityFadeTimer += Time.deltaTime;
					if(unityFadeTimer > 1f){
						unityFadeIn = false;
					}
				}
			}
			else {
				unityAlpha = Mathf.Lerp(unityAlpha, 0f, Time.deltaTime * 2);
	       		startUpUnity.guiTexture.color = new Color(0.5f, 0.5f, 0.5f,unityAlpha);
	        	if(unityAlpha < 0.01f){
					startUpUnity.guiTexture.color = new Color(0.5f, 0.5f, 0.5f,0);
					fadeTimer = 0;
					titleFadeIn = true;
					unityTexture = false;
					middleWait = true;
				}
			}
		}
		else if ( middleWait ){
			middleTimer += Time.deltaTime;
			if (middleTimer > 1.1f){
				middleWait = false;
				titleTexture = true;
			}
		}
		else if ( titleTexture ){
			if( titleFadeIn ){
				titleAlpha = alphaFuncion();
	        	startUpTitle.guiTexture.color = new Color(0.5f, 0.5f, 0.5f,titleAlpha);
	        	if(titleAlpha > 0.48f){
					titleFadeWait = true;
					titleFadeIn = false;
					fadeTimer = 0;
					startUpTitle.guiTexture.color = new Color(0.5f, 0.5f, 0.5f,0.5f);
				}
			}
			else if (titleFadeWait){
				titleFadeTimer += Time.deltaTime;
				if(titleFadeTimer > 1.5f){
					titleMove = true;
					titleFadeWait = false;
				}
			}
			else if ( titleMove ) {
				startUpTitle.transform.position = titleStartPos - (titleStartPos - titleMoveTo) * titleSizeTimer;
				titleSize = titleStartSize - (titleStartSize - titleSizeTo) * titleSizeTimer;
				startUpTitle.guiTexture.pixelInset = new Rect (Screen.width * titleSize.x / 1920f / -2f, Screen.height * titleSize.y / 1080f / -2f, Screen.width * titleSize.x / 1920f, Screen.height * titleSize.y / 1080f);
				if(titleSizeTimer >= 0.99f){
					titleTexture = false;
					end = true;
				}
				titleSizeTimer += Time.deltaTime;
			}
		}
		if (end){
			Screen.lockCursor = false;
			Application.LoadLevel(0);
		}
			
	}
	
	float alphaFuncion(){
		fadeTimer += Time.deltaTime / fadeSpeed;
		return (-1f / (fadeTimer - 1.5f) - 0.65f) / 2f;
	}
}
