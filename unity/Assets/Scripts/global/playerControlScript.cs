using UnityEngine;
using System.Collections;

public class playerControlScript : MonoBehaviour
{	
	float movementSpeedDefault = 3.6f;
	float movementSpeed;
	float crouchSpeedMultiplier = 0.16f;
	float sprintMaxSpeed = 6f;
	float sprintAcceleration = 12f;
	float sprintDeceleration = -16f;
	float verticalVelocity;
	float jumpSpeedDefault = 2.5f;
	float jumpSpeed;
	float verticalRotation = 0;
	float mouseUpDownRange = 85.0f;
	public float mouseSpeed = 3f;
	public float joystickSpeed = 3f;
	float staminaDefault = 14.5f;
	public float stamina = 14.5f;
	float forwardSpeed;
	float horizontalSpeed;
	float horizontalMovementSpeed = 2f;
	
	Vector3 playerSizeDefault = new Vector3 (1.0f, 0.9f, 1.0f);
	Vector3 playerSize;
	float crouchPlayerSize = 0.6f;
	
	public Vector3 enviromentalMovement = new Vector3 (0, 0, 0);
	
	bool mouseOn = true;
	bool crouchJumped = false;
	
	public bool canStandUp = true;
	public bool canMove = true;
	public bool joystick = false;
	
	
	// Use this for initialization
	void Start ()
	{
		playerSize = playerSizeDefault;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CharacterController cc = GetComponent<CharacterController>();
		
		if (canMove)
		{
			//Crouch
			if (Input.GetButton("Crouch"))
			{
				// Ha le kell guggolni és még nem guggolt le akkor leguggol: lassan csökkenti a méretét
				if (playerSize.y >= crouchPlayerSize) playerSize.y -= 0.03f;
				movementSpeed = movementSpeedDefault * crouchSpeedMultiplier;//* crouchSpeedMultiplier;
				horizontalMovementSpeed = 1f;					// guggolás közbeni mozgás lassítása
				playerSize.y = Mathf.Clamp(playerSize.y, crouchPlayerSize, playerSizeDefault.y);
				transform.localScale = playerSize;
				rigidbody.AddForce(0, 0.01f, 0);				//kicsit lök felfelé, hogy ne essen át a padlón (bug fix)
			}
			else if (playerSize.y < playerSizeDefault.y && canStandUp)							// ha fel kell állni, és fel tud; a canStandUp-hoz egy külső (objektumok alatti) hitbox scriptje fér hozzá
			{
				playerSize.y += 0.03f;
				playerSize.y = Mathf.Clamp(playerSize.y, crouchPlayerSize, playerSizeDefault.y);
				horizontalMovementSpeed = 2f;					// minden tulajdonság visszaállítása
				transform.localScale = playerSize;
			}
			
			Camera.main.transform.localPosition = new Vector3(0, 2 * playerSize.y - 0.1f, 0f); 		// kamera beállítása a játékos magasságától függően
			
			//sprint
				if(Input.GetButton("Sprint") && stamina > 0 && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Joystick Vertical") > 0) && cc.isGrounded && !Input.GetButton("Crouch") && canStandUp) // ha minden meg van ahhoz, hogy sprinteljen
				{
					if (movementSpeed < sprintMaxSpeed) movementSpeed += sprintAcceleration * Time.deltaTime;
					if (movementSpeed > sprintMaxSpeed) movementSpeed = sprintMaxSpeed;
					stamina -= Time.deltaTime;					// begyorsítja, és veszít a staminából
					if(stamina < 0)
					{
						movementSpeed = movementSpeedDefault;		// ha kifárad lelassítja (bug fix)
						if(Input.GetButtonUp("Sprint")) stamina -= 5.0f;
					}
				}
				else if(cc.isGrounded)										// ha nem sprintel visszaállítja normális sebességre és visszatölti a staminát
				{
					stamina += Time.deltaTime;
					if (movementSpeed > movementSpeedDefault) movementSpeed += sprintDeceleration * Time.deltaTime;
					if (movementSpeed < movementSpeedDefault && (!Input.GetButton("Crouch") || !canStandUp)) movementSpeed = movementSpeedDefault;
					if(!Input.GetButton("Crouch") && canStandUp) movementSpeed = Mathf.Clamp(movementSpeed, movementSpeedDefault, movementSpeedDefault + 3.0f);
					else if (!canStandUp) movementSpeed = movementSpeedDefault / 3.0f;
					else movementSpeed = Mathf.Clamp(movementSpeed, movementSpeedDefault - 2.0f, movementSpeedDefault + 3.0f);
				}
			stamina = Mathf.Clamp(stamina, -5, staminaDefault);
Debug.Log("Speed: " + movementSpeed + " Stamina: " + stamina);
			
			//jump
			if(cc.isGrounded)
				verticalVelocity = 0;
			
			verticalVelocity += Physics.gravity.y * Time.deltaTime; //gravitáció
//Debug.Log (verticalVelocity);
			
			jumpSpeed = jumpSpeedDefault + movementSpeed * (movementSpeed - 1.2f * Mathf.Sqrt(movementSpeed)) / 16; // ugrás sebessége (magassága), a játékos sebességéből levezetve (bonyolult, de így reális)
			
	
			
			if(Input.GetButtonDown("Jump") && cc.isGrounded && !Input.GetButton("Crouch") ) // ha ugorhat
			{
				verticalVelocity = jumpSpeed; // ugrik és fárad
				stamina -= 0.3f;
			}
			
			if(!cc.isGrounded){						// ha a levegőben van
				if(!crouchJumped && Input.GetButtonDown("Crouch")){ // ugrás-gugolás
					verticalVelocity += 2.0f;	// guggolással lök egyet (bug fix)
					crouchJumped = true;		// csak egyszer lehet ugrani
				}
			}
			else {
				crouchJumped = false;			// ugrás-gugolás számláló nullázása
			}
	
			
			
			//rotate camera
			if(mouseOn && !joystick){						// ha mozoghat a kamera, és nem a joystick mozog
				float mouseX = Input.GetAxis("Mouse X") * mouseSpeed;			// x irányba forgatja a játékost
				transform.Rotate(0, mouseX, 0);
				
				verticalRotation += Input.GetAxis("Mouse Y") * mouseSpeed * -1;	// y irányba forgatja a kamerát
				verticalRotation = Mathf.Clamp(verticalRotation, -mouseUpDownRange, mouseUpDownRange);
				Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
			}
			else if(mouseOn && joystick)					// ha a joystick mozog
			{
				float mouseX;
				mouseX = Input.GetAxis("Joystick X") * joystickSpeed;
				transform.Rotate(0, mouseX, 0);
				
			
				verticalRotation += Input.GetAxis("Joystick Y") * joystickSpeed;
				verticalRotation = Mathf.Clamp(verticalRotation, -mouseUpDownRange, mouseUpDownRange);
				Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
			}
			
			
			// Egér eltüntetése, ha a menü bekapcsol
			if(GetComponent<pauseMenuScript>().paused)
			{
				mouseOn = false;
				Screen.lockCursor = false;
			}
			else
			{
				Screen.lockCursor = true;
				mouseOn = true;
			}
				
			
			//move		
			if(cc.isGrounded)				
			{
				if (joystick) // játékos irányítása joystick-el
				{
					forwardSpeed = Input.GetAxis("Joystick Vertical") * movementSpeed;
					horizontalSpeed = Input.GetAxis("Joystick Horizontal") * (movementSpeed / horizontalMovementSpeed);			// oldalra lassabban mozog
				}
				else { // játékos irányítása billenytűzettel
					forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
					horizontalSpeed = Input.GetAxis("Horizontal") * (movementSpeed / horizontalMovementSpeed);
				}
			}
		
//Debug.Log(horizontalSpeed + " " + horizontalMovementSpeed);
//Debug.Log(forwardSpeed);
			
			Vector3 speed = new Vector3(horizontalSpeed, verticalVelocity, forwardSpeed);		// irányok egyesítése vector-ba
			speed = transform.rotation * speed + enviromentalMovement * 60f;					// fordítással elforgatva a vector, a környezeti sebességgel "meglökve" (platformok)
			cc.Move( speed * Time.deltaTime );													// objektum mozgatása
			
			enviromentalMovement = new Vector3(0,0,0);											// környezetei nullázása frame-enként (ne gyorsuljon; bug fix)
		}
	}
}
