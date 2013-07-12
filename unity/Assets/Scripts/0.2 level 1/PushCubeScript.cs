using UnityEngine;
using System.Collections;

public class PushCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Resolution[] resolutions = Screen.resolutions;
       /* foreach (Resolution res in resolutions) {
            print(res.width + "x" + res.height);
        }*/
		Debug.Log(resolutions.Length);
		Debug.Log(resolutions[resolutions.Length-1].width + " x " + resolutions[resolutions.Length-1].height);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnCollisionEnter (Collision col) {

		if(col.gameObject.name == "Player"){
			foreach(ContactPoint contact in col.contacts)
			{
				if(contact.thisCollider == collider)
					rigidbody.AddForce(contact.point);
			}
		}
		
	}
}
