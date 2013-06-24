using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {
	
	public GameObject prefabCube;
	float timer = 1.4f;
	float timerLimit = 1.5f;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > timerLimit)
		{
			Instantiate(prefabCube, new Vector3 (50.0f * Random.value - 25.0f, 25.0f, 40.0f * Random.value) , Random.rotation);
			timer = 0;
			timerLimit -= 0.4f;
			if(timerLimit <= 0.2f)
				timerLimit = 0.2f;
		}
	}
}
