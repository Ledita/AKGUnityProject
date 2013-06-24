using UnityEngine;
using System.Collections;

public class setupScript : MonoBehaviour {
	
	public GameObject cubePrefab;

	
	// Use this for initialization
	void Start () {
			for(float i = 0; i <= 10; i += 2){
				for(float j = 0; j <= 10; j += 2){
					if(i != 0 || j != 0)
					Instantiate(cubePrefab, new Vector3(i, Random.value * 0.2f - 0.55f, j), Quaternion.identity);
				}
			}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
