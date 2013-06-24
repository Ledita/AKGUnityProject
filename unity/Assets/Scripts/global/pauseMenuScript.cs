using UnityEngine;
using System.Collections;

public class pauseMenuScript : MonoBehaviour 
{
    public GUISkin myskin;

    private Rect windowRect;
    public bool paused = false;
	public bool canPause = true;

    private void Start()
    {
        windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 145);
    }



    private void Update()
    {

            if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("JoystickPause")) && canPause)
            {
                if (paused){
                    paused = false;
					Time.timeScale = 1;
				}
                else{
                    paused = true;
					Time.timeScale = 0;
				}

            }
    }

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
            paused = false;
        }
		if (GUILayout.Button("Restart level"))
		{
			Time.timeScale = 1;
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
}
