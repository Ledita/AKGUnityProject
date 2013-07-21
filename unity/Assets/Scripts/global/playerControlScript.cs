using UnityEngine;
using System.Collections; 

public class playerControlScript : MonoBehaviour
{	
	float movementSpeedDefault = 3.6f;
	float movementSpeed;
	
	float crouchSpeedMultiplier = 0.16f;
	float crouchPlayerSize = 0.6f;
	
	float sprintMaxSpeed = 6f;
	float sprintAcceleration = 14f;
	float sprintDeceleration = -16f;
	float staminaDefault = 14.5f;
	public float stamina = 14.5f;
	
	public float verticalVelocity;
	float jumpSpeedDefault = 2.5f;
	float jumpSpeed;
	float verticalRotation = 0;
	Quaternion jumpRotation;
	float wallJumpTimer = 0;
	
	float mouseUpDownRange = 85.0f;
	public float mouseSpeed = 3f;
	public float joystickSpeed = 3f;
	
	public float forwardSpeed;
	public float horizontalSpeed;
	float horizontalMovementSpeed = 2f;
	public float playerSpeed;
	
	float footstepTimer = 0;
	float stepPrev = 0;
	public float volume = 1f;

	float FPS;
	
	Vector3 playerSizeDefault = new Vector3 (1.0f, 0.9f, 1.0f);
	Vector3 playerSize;
	
	public Vector3 enviromentalMovement = new Vector3 (0, 0, 0);
	
	bool mouseOn = true;
	bool crouchJumped = false;
	bool isCrouching = false;
	bool landPre = true;
	bool wallJump = false;
	
	public bool canStandUp = true;
	public bool canMove = true;
	public bool joystick = false;
	
	public AudioClip[] footstepsSound;
	public AudioClip fallSound1;
	public AudioClip fallSound2;
	
	public GameObject aimPrefab;
	public GameObject inventoryPrefab;
	
	public float pushPower = 2.0f;
	public bool grounded;
	
	
	
	// Use this for initialization
	void Start ()
	{
		playerSize = playerSizeDefault;
		Instantiate(aimPrefab, new Vector3 (0.5f, 0.5f, 0), Quaternion.Euler(0,0,0));
		Instantiate(inventoryPrefab, new Vector3 (1.16f, 1.28f, 0), Quaternion.Euler(0,0,0));
	}
	
	// Update is called once per frame
	void Update ()
	{
		CharacterController cc = GetComponent<CharacterController>();
		grounded = cc.isGrounded;
		if(Time.deltaTime != 0)
			FPS = 1/Time.deltaTime;
		
		// Egér eltüntetése, ha a menü bekapcsol

		
		if (canMove)
		{
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
			/*
			if(isCrouching){
				float rayHeight = 1f;
				Vector3 rayNDown = new Vector3 ((transform.position.x + (cc.radius - 0.1f)), transform.position.y, transform.position.z); Vector3 rayNUp = new Vector3 ((transform.position.x + (cc.radius - 0.1f)), transform.position.y + rayHeight, transform.position.z);
				Vector3 raySDown = new Vector3 ((transform.position.x - (cc.radius - 0.1f)), transform.position.y, transform.position.z); Vector3 raySUp = new Vector3 ((transform.position.x - (cc.radius - 0.1f)), transform.position.y + rayHeight, transform.position.z);
				Vector3 rayEDown = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (cc.radius - 0.1f)); Vector3 rayEUp = new Vector3 (transform.position.x, transform.position.y + rayHeight, transform.position.z + (cc.radius - 0.1f));
				Vector3 rayWDown = new Vector3 (transform.position.x, transform.position.y, transform.position.z - (cc.radius - 0.1f)); Vector3 rayWUp = new Vector3 (transform.position.x, transform.position.y + rayHeight, transform.position.z - (cc.radius - 0.1f));
				if(Physics.Raycast(rayEDown, rayEUp, rayHeight) || Physics.Raycast(rayWDown, rayWUp, rayHeight) || Physics.Raycast(raySDown, raySUp, rayHeight) || Physics.Raycast(rayNDown, rayNUp, rayHeight))
					canStandUp = false;
				else
					canStandUp = true;
			}
			*/
			
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
				isCrouching = true;
			}
			else if (playerSize.y < playerSizeDefault.y && canStandUp)							// ha fel kell állni, és fel tud; a canStandUp-hoz egy külső (objektumok alatti) hitbox scriptje fér hozzá
			{
				playerSize.y += 0.03f;
				playerSize.y = Mathf.Clamp(playerSize.y, crouchPlayerSize, playerSizeDefault.y);
				horizontalMovementSpeed = 2f;					// minden tulajdonság visszaállítása
				transform.localScale = playerSize;
			}
			else
				isCrouching = false;
			
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
//Debug.Log("Speed: " + movementSpeed + " Stamina: " + stamina);
			
			//jump
			if(cc.isGrounded)
				verticalVelocity = 0;
			
			verticalVelocity += Physics.gravity.y * Time.deltaTime; //gravitáció
//Debug.Log (verticalVelocity);
			
			jumpSpeed = jumpSpeedDefault + movementSpeed * (movementSpeed - 1.2f * Mathf.Sqrt(movementSpeed)) / 16; // ugrás sebessége (magassága), a játékos sebességéből levezetve (bonyolult, de így reális)
			
	
			
			if(Input.GetButtonDown("Jump") && (cc.isGrounded || wallJump) && !Input.GetButton("Crouch") ) // ha ugorhat
			{
				verticalVelocity = jumpSpeed; // ugrik és fárad
				stamina -= 0.3f;
				if(wallJump){
					stamina -= 0.1f;
					verticalVelocity += 1f;
					AudioSource.PlayClipAtPoint(fallSound2, transform.position, volume);
					wallJump = false;
					wallJumpTimer = 0;
				}
				jumpRotation = transform.rotation;
			}
			
			if(!cc.isGrounded){						// ha a levegőben van
				if(!crouchJumped && Input.GetButtonDown("Crouch")){ // ugrás-guggolás
					verticalVelocity += 1.0f;	// guggolással lök egyet (bug fix)
					crouchJumped = true;		// csak egyszer lehet ugrani
				}
			}
			else {
				wallJump = false;
				crouchJumped = false;			// ugrás-guggolás számláló nullázása
			}
	
			//wall jump timer
			if(wallJump){
				wallJumpTimer += Time.deltaTime;
				if(wallJumpTimer > 0.5f){
					wallJump = false;
				}
			}
			else
				wallJumpTimer = 0;
			
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
			if(cc.isGrounded && (cc.isGrounded != landPre || (Input.GetAxis("Joystick Vertical") == 0 && Input.GetAxis("Joystick Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0))){
				horizontalSpeed = 0;
				forwardSpeed = 0;
			}
//Debug.Log(horizontalSpeed + " " + horizontalMovementSpeed);
//Debug.Log(forwardSpeed);
			
			Vector3 speed = new Vector3(horizontalSpeed, verticalVelocity, forwardSpeed);		// irányok egyesítése vector-ba
			//if(cc.isGrounded)
				speed = transform.rotation * speed + enviromentalMovement * FPS;					// fordítással elforgatva a vector, a környezeti sebességgel "meglökve" (platformok)
			/*else
				speed = jumpRotation * speed + enviromentalMovement * FPS; */
//Debug.Log(FPS);
			
			cc.Move( speed * Time.deltaTime );													// objektum mozgatása
			
			enviromentalMovement = new Vector3(0,0,0);											// környezetei nullázása frame-enként (ne gyorsuljon; bug fix)
			
			//sounds
			if(cc.isGrounded){
				playerSpeed = Mathf.Sqrt( forwardSpeed * forwardSpeed + horizontalSpeed * horizontalSpeed );
				
				/*
				footstepTimer += Mathf.Abs( forwardSpeed );
				if(Mathf.Abs( forwardSpeed ) < 1){
					footstepTimer += Mathf.Abs( horizontalSpeed );
				}
				*/
				footstepTimer += playerSpeed;
				if(footstepTimer > 90f)
				{
					footstepTimer = 0;
					AudioSource.PlayClipAtPoint(footstepsSound[ Random.Range(0, footstepsSound.Length) ],  transform.position, volume);
				}
				if(stepPrev == 0 && Mathf.Abs( forwardSpeed ) + Mathf.Abs( horizontalSpeed ) != 0){
					footstepTimer = 0;
					AudioSource.PlayClipAtPoint(footstepsSound[ Random.Range(0, footstepsSound.Length) ],  transform.position, volume);
				}
				stepPrev = Mathf.Abs( forwardSpeed ) + Mathf.Abs( horizontalSpeed );
				if(landPre == false){
					if(verticalVelocity < -8f)	//nagy esés
						AudioSource.PlayClipAtPoint(fallSound1, transform.position, volume - 0.4f);
					else if(verticalVelocity < -6f) // kis esés
						AudioSource.PlayClipAtPoint(fallSound2, transform.position, volume);
					
				}
			}
			landPre = cc.isGrounded;
		}
		else{
			playerSpeed = 0;
			cc.Move( enviromentalMovement );
		}
	}
	
	
	// pushing objects
    void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        
        if (hit.moveDirection.y < -0.3F)
            return;
        
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
	}
    
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "wall")
			wallJump = true;
	}
}
