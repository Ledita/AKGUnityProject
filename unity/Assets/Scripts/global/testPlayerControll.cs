using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour {

	public float movementSpeedBasic = 4.0f;
	float movementSpeed;
	public int mouseSpeed = 3;
	public int joysticSpeed = 2;
	public float mouseUpDownRange = 60.0f;
	public float jumpSpeedBasic = 2.5f;
	float jumpSpeed = 2.5f;
	
	public GameObject flashLight;
	bool flashlightOn = true;
	
	public GameObject cube;
	
	float cubeRotation = 0.0f;
	
	float stamina = 10.0f;	
	float verticalRotation = 0;
	float verticalVelocity = 0;
	Vector3 cameraHeight = new Vector3 (0, 1.72f, 0);
	Vector3 playerSize = new Vector3 (1.0f, 0.9f, 1.0f);
	float duringJumpSpeed = 4.0f;
	bool grounded_A = true;
	bool grounded_B = true;
	bool grounded_C = true;
	
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController cc = GetComponent<CharacterController>();
		// Crouch
		if(Input.GetButton("Crouch"))
		{
			playerSize = new Vector3(1.0f, 0.5f, 1.0f);
			cameraHeight = new Vector3(0, 0.9f, 0);
			movementSpeed = movementSpeedBasic - 2.0f;
		}
		else{
			playerSize = new Vector3 (1.0f, 0.9f, 1.0f);
			cameraHeight = new Vector3(0, 1.72f, 0);
			movementSpeed = movementSpeedBasic;
		}
		transform.localScale = playerSize;
		Camera.main.transform.localPosition = cameraHeight;
		
		//sprint
		if(Input.GetButton("Sprint") && !Input.GetButton("Crouch") && stamina > 0)
		{
			movementSpeed = movementSpeedBasic + 4;
			stamina -= Time.deltaTime;
			jumpSpeed = jumpSpeedBasic + 0.4f;
			if(stamina < 0)
			{
				movementSpeed = movementSpeedBasic;	
				if(Input.GetButtonUp("Sprint")) stamina -= 5;
			}
		}
		else if(!Input.GetButton("Crouch"))
		{
			stamina += Time.deltaTime;
			movementSpeed = movementSpeedBasic;
			jumpSpeed = duringJumpSpeed;
		}
		stamina = Mathf.Clamp(stamina, -5, 3.5f);
		//Debug.Log(stamina);
		
		//rotate
		float mouseX = Input.GetAxis("Mouse X") * mouseSpeed + Input.GetAxis("Joystick X") * joysticSpeed;
		transform.Rotate(0, mouseX, 0);
		
		verticalRotation += Input.GetAxis("Mouse Y") * mouseSpeed * -1 + Input.GetAxis("Joystick Y") * joysticSpeed;
		verticalRotation = Mathf.Clamp(verticalRotation, -mouseUpDownRange, mouseUpDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		
		//jump
		verticalVelocity += Physics.gravity.y * Time.deltaTime; //gravitáció
		
		if(Input.GetButtonDown("Jump") && cc.isGrounded ) //ugrás
		{
			verticalVelocity = jumpSpeed;
			duringJumpSpeed = movementSpeed;
			stamina -= 0.5f;
		}
		if(!cc.isGrounded)						//levegőben állandó sebesség
		{
			movementSpeed = duringJumpSpeed;
		}
		if(grounded_C){							//ugrás változás vizsgáló
			grounded_A = cc.isGrounded;
			grounded_C = false;
		}
		else
		{
			grounded_B = cc.isGrounded;
			grounded_C = true;
		}
		
		//move
		
		float verticalSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float horizontalSpeed = Input.GetAxis("Horizontal") * (movementSpeed - 1.0f);
		
		//if(grounded_A == grounded_B) verticalSpeed += 0; // extra move before and after jump
		//else verticalSpeed += movementSpeed;
				
		Vector3 speed = new Vector3(horizontalSpeed, verticalVelocity, verticalSpeed);
		speed = transform.rotation * speed;
		cc.Move( speed * Time.deltaTime );
		
		//Debug.Log(verticalVelocity);
		
		//light
		if(Input.GetButtonDown("Action")){
			if(flashlightOn)
			{
				flashLight.light.intensity = 0.0f;
				flashlightOn = false;
			}
			else
			{
				flashLight.light.intensity = 0.5f;
				flashlightOn = true;
			}
			
			Debug.Log ("F");
			//cubeRotation += 5.0f;
			//cube.transform.localRotation = Quaternion.Euler(cubeRotation, 0, 0);
		}
	}
}