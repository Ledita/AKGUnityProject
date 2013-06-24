using UnityEngine;
using System.Collections;

public class Part5Script : MonoBehaviour {
	
	GameObject endTrigger;
	
public GameObject prefabFallingCube;
	public GameObject prefabFloorCube;
	bool start = false;
	float timer = 0;
	GameObject[] fallingfloorCubes;
	GameObject[] fallingCubes;
	GameObject player;

	// Use this for initialization
	void Start () {
		endTrigger = GameObject.Find("endTrigger");
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(start && !player.GetComponent<pauseMenuScript>().paused){
			timer += Time.deltaTime;
			if(timer > 0.1f)
			{
				Instantiate(prefabFallingCube, new Vector3 (3f * Random.value + 31, 9.9f, 14.5f * Random.value - 49.5f) , Random.rotation); 
				timer = 0;
			}
			if(Random.value < 0.007f){
				Instantiate(prefabFallingCube, new Vector3 (53f * Random.value - 24f, 9.9f, 15f * Random.value - 50f), Random.rotation);
			}
		}
		else timer = 0;
		
	}
	
	void OnTriggerEnter(Collider fallen){
		if(fallen.gameObject.name == "Player"){
			fallen.transform.position = new Vector3(40f, 4.6f, -42f);
			fallen.transform.rotation = Quaternion.Euler(0, -90f, 0);
			setup();
			fallen.GetComponent<playerControlScript>().stamina = 3.5f;
			endTrigger.GetComponent<endFallingCubesScript>().done = false;
		}
		else Destroy(fallen.gameObject);
	}
	
	public void setup(){
		start = true;
		fallingfloorCubes = GameObject.FindGameObjectsWithTag("FallingFloor");
		
		for( int i = 0; i < fallingfloorCubes.Length; i++){
			Destroy(fallingfloorCubes[i]);
		}
		fallingCubes = GameObject.FindGameObjectsWithTag("FallingCube");
		for(int i = 0; i < fallingCubes.Length; i++){
			Destroy(fallingCubes[i]);
		}
		
					
		for( int i = 29; i > -24; i -= 2)
		{
			for( float j = -35.5f; j > -50f; j -= 2)
			{
				Instantiate(prefabFloorCube, new Vector3(i, Random.value * 0.1f + 3f, j), Quaternion.identity);
			}
		}		
	}
	
	public void end(){
		for( int i = 0; i < 30; i++){
			Instantiate(prefabFallingCube, new Vector3(Random.value * 53f - 24f, 9.9f, Random.value * 14.5f - 50f), Random.rotation);
		}
		for( int i = 0; i < 10; i++){
			Instantiate(prefabFallingCube, new Vector3(Random.value * 12f - 32f, 9.9f, Random.value * 14.5f - 50f), Random.rotation);
		}
		start = false;
	}
}
