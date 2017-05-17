using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController_2 : MonoBehaviour {

	float verticalVelocity = 0;
	float jumpSpeed = 4.0f;
	CharacterController cc;
	Vector3 speed;
	public HUDScript hud;
	//bool crouching = false;
	float amountRot = 0.0f;
	bool HasFlashlight = true;
	GameObject Flashlight;
	Light playerFlashlight;
	AudioSource bcs;
	public AudioSource click;
	//Vector3 sprint;
	bool sprint;

	void Awake()
	{
		hud = hud.GetComponent<HUDScript>();
		bcs = GetComponent<AudioSource>();
		click = click.GetComponent<AudioSource>();
	} 

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();

		Flashlight = GameObject.FindWithTag ("Flashlight");
		GameObject testLight = GameObject.Find ("Spotlight_Player");
		playerFlashlight = (Light)testLight.GetComponent (typeof(Light));
		//  Light light = (Light)goLight.GetComponent(typeof(Light));
		playerFlashlight.intensity = 0;

		//Screen.lockCursor = true; //OBSOLETE
		Cursor.lockState = CursorLockMode.Locked; //makes mouse not visible
		if (cc == null) {
			//handle this - will crash ... could "require" it - RequireComponent(typeof(CharacterController))....
		}
	}

	// Update is called once per frame
	void Update()
	{

		if (!HUDScript.isPaused && !HUDScript.isGameOver) {
			//Rotation
			float rotateX = Input.GetAxis ("Mouse X") * 2.0f; //increase mouse sensitivity w/ 2.0
			//float rotateY = Input.GetAxis ("Mouse Y");
			Vector3 rotate = new Vector3 (0, rotateX, 0);//cc.center.x, 0);//rotateX, 0);
			transform.Rotate (rotate);


		


		//movement 
		//can multiply these to make them go faster
		float forwardSpeed = Input.GetAxis ("Vertical") * 2.0f;
		float sideSpeed = Input.GetAxis ("Horizontal") * 2.0f;
		//x(left-right), y(up-down), z(forward-back)

		verticalVelocity += Physics.gravity.y * Time.deltaTime; //increase speed down over time 
		if (Input.GetButtonDown ("Crouch") && cc.isGrounded == true) {
			cc.height = 0f;
			//cc.radius = 0.1f;

		} else if (Input.GetButtonUp ("Crouch") && cc.isGrounded == true) {

			Vector3 temp = new Vector3 (transform.position.x, transform.position.y + 0.8f, transform.position.z);
			Ray crouchRay = new Ray (temp, transform.up);
			RaycastHit crouchHit;

			if (Physics.Raycast (crouchRay, out crouchHit, 2.0f)) {

				//Debug.Log ("Hit");
				Debug.DrawLine (crouchRay.origin, crouchHit.point, Color.green);
				cc.height = 0f;
			} else {
				cc.height = 2.5f;
			}

		}


		//
		//	unCrouch();
		//}

		

		if (Input.GetButtonDown ("Light") && HasFlashlight == true) {
			click.Play();
			if (playerFlashlight.intensity == 0) {
				playerFlashlight.intensity = 7;
			} else {
				playerFlashlight.intensity = 0;
			}
		}


		if (Input.GetButtonDown ("Jump") && cc.isGrounded == true) { //isgrounded means the character is on the ground
			verticalVelocity = jumpSpeed;
		} else if (Input.GetKey (KeyCode.Escape) && !HUDScript.isPaused && !HUDScript.isGameOver) {
			hud.PauseGame ();
		}

		

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			//speed = sprint;
			sprint = true;			} 
		else if (Input.GetKeyUp(KeyCode.LeftShift)){
			sprint = false;
		}

			if (!sprint) {
				speed = new Vector3 ((sideSpeed * 2), verticalVelocity, (forwardSpeed * 2));
				//sprint = speed * 10;
				//editing speed to match camera direction
				speed = transform.rotation * speed;
			} else {
				speed = new Vector3 ((sideSpeed * 5), verticalVelocity, (forwardSpeed * 5));
				//sprint = speed * 10;
				//editing speed to match camera direction
				speed = transform.rotation * speed;
			}

		//cc.SimpleMove (speed);   //cant use simple move if we want any Y components (jumping/falling, etc)
		//simple move has gravity included
		if (!HUDScript.isPaused && !HUDScript.isGameOver) {
			cc.Move (speed * Time.deltaTime);
		}
	}
	/*
void unCrouch()
{
	cc.height = 2 ;
	crouching = false;
}
		
*/


}
}
