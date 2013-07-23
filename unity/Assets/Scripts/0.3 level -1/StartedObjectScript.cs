using UnityEngine;
using System.Collections;

public class StartedObjectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
}
