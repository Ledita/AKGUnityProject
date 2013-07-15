using UnityEngine;
using System.Collections;

public class pressurePlateHitobxScript : MonoBehaviour {
	
	public GameObject PressurePlate;
	public string name;
	

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.name == name || obj.gameObject.tag == name)
			PressurePlate.GetComponent<PressurePlateScript>().AddObject(obj.gameObject);
	}
	void OnTriggerExit(Collider obj){
		if(obj.gameObject.name == name || obj.gameObject.tag == name)
			PressurePlate.GetComponent<PressurePlateScript>().RemoveObject(obj.gameObject);
	}
}
