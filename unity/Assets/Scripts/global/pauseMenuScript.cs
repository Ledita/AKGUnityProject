using UnityEngine;
using System.Collections;

public class pauseMenuScript : MonoBehaviour 
{
    //public GUISkin myskin;

    //private Rect windowRect;
    public bool paused = false;
	public bool canPause = true;
	public bool swichPause = false;
	public GameObject pauseMenu;
	GameObject[] pauseMenuElements = {null, null, null, null, null, null, null};
	GameObject pauseMenuObj;
	bool pauseStart = false;
	bool pauseEnd = false;
	float menuXPos = -0.38f;
	float textHeight = 100f;
	

    private void Start()
    {
        //windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 145);
		Instantiate(pauseMenu, new Vector3(menuXPos, 0, 0), Quaternion.Euler(0,0,0));
		pauseMenuObj = GameObject.Find("PauseMenu(Clone)");
		pauseMenuElements[0] = GameObject.Find("background");
		pauseMenuElements[0].guiTexture.pixelInset = new Rect(-Screen.width, Screen.height / -2f, Screen.width * 2655f / 1920f, Screen.height);
		pauseMenuElements[1] = GameObject.Find("Paused");
		pauseMenuElements[1].guiTexture.pixelInset = new Rect(0, Screen.height * (textHeight + 5) / 2 / -1080f, Screen.width * 345f / 1920f, Screen.height * (textHeight + 5) / 1080f);
		pauseMenuElements[2] = GameObject.Find("Resume");
		pauseMenuElements[2].guiTexture.pixelInset = new Rect(0, Screen.height * textHeight /2 / -1080f, Screen.width * 327 / 1920f, Screen.height * textHeight / 1080f);
		pauseMenuElements[3] = GameObject.Find("Restart");
		pauseMenuElements[3].guiTexture.pixelInset = new Rect(0, Screen.height * textHeight /2 / -1080f, Screen.width * 387 / 1920f, Screen.height * textHeight / 1080f);
		pauseMenuElements[4] = GameObject.Find("pauseOptions");
		pauseMenuElements[4].guiTexture.pixelInset = new Rect(0, Screen.height * textHeight /2 / -1080f, Screen.width * 383 / 1920f, Screen.height * textHeight / 1080f);
		pauseMenuElements[5] = GameObject.Find("Main Menu");
		pauseMenuElements[5].guiTexture.pixelInset = new Rect(0, Screen.height * textHeight /2 / -1080f, Screen.width * 650 / 1920f, Screen.height * textHeight / 1080f);
		pauseMenuElements[6] = GameObject.Find("Quit");
		pauseMenuElements[6].guiTexture.pixelInset = new Rect(0, Screen.height * textHeight /2 / -1080f, Screen.width * 215 / 1920f, Screen.height * textHeight / 1080f);
		
    }



    private void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("JoystickPause") || swichPause) && canPause)
        {
			swichPause = false;
            if (paused){
                paused = false;
				Time.timeScale = 1;
				GetComponent<playerControlScript>().canMove = true;
				menuXPos = -0.38f;
			}
            else{
                paused = true;
				Time.timeScale = 0;
				GetComponent<playerControlScript>().canMove = false;
				Screen.lockCursor = false;
				menuXPos = 0;
			}
			pauseMenuObj.transform.position = new Vector3(menuXPos, 0, 0);
        }
    }
	
	/*
    private void OnGUI()
    {
        if (paused)
            windowRect = GUI.Window(0, windowRect, windowFunc, "Pause Menu");
    }

    private void windowFunc(int id)
    {
        if (GUILayout.Button("Resume"))
        {
			Time.timeScale = 1;
			GetComponent<playerControlScript>().canMove = true;
            paused = false;
        }
		if (GUILayout.Button("Restart level"))
		{
			Time.timeScale = 1;
			GetComponent<playerControlScript>().canMove = true;
			Application.LoadLevel(Application.loadedLevel);
		}
        if (GUILayout.Button("Options"))
        {

        }
		if (GUILayout.Button("Main menu"))
		{
			Time.timeScale = 1;
			Application.LoadLevel(0);
		}
        if (GUILayout.Button("Quit"))
        {
			Application.Quit();
        }
    }
    */
}
