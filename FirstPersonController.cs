using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour {

	float verticalVelocity = 0;
	float jumpSpeed = 4.0f;
	CharacterController cc;
	Vector3 speed;
    public HUDScript hud;
    //bool crouching = false;
	bool RotateBook = false;
	float amountRot = 0.0f;
	GameObject book;
	GameObject Bookcase;
	bool moveBookcase = false;
	bool HasFlashlight = false;
	GameObject Flashlight;
	Light playerFlashlight;
	GameObject Skulls;
    AudioSource bcs;
    public AudioSource click;

    void Awake()
    {
        hud = hud.GetComponent<HUDScript>();
        bcs = GetComponent<AudioSource>();
        click = click.GetComponent<AudioSource>();
    } 

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		book = GameObject.FindWithTag ("Book");
		Bookcase = GameObject.FindWithTag ("Bookcase");
		Flashlight = GameObject.FindWithTag ("Flashlight");
		GameObject testLight = GameObject.Find ("Spotlight_Player");
		playerFlashlight = (Light)testLight.GetComponent (typeof(Light));
		Skulls = GameObject.FindWithTag ("SkullGroup");
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
        
			/*
            //raycast detection (for sanity)
            RaycastHit hit;

            Vector3 p1 = transform.position + cc.center;
            float distanceToObstacle = 0;

            // Cast a sphere wrapping character controller 5 meters forward
            // to see if it is about to hit anything.
            if (Physics.SphereCast(p1, cc.height / 2, transform.forward, out hit, 1.5f)) {
                //Debug.DrawRay(transform.position, (cc.transform.position - transform.position), Color.green); // show path of SphereCast
                //Debug.Log("rayHit.collider.tag " + hit.collider.tag); // print collider info

                if (hit.collider.tag == "Enemy" ) {
                    //lower sanity
                    distanceToObstacle = hit.distance;
                    hud.DecSanity(0.4f);
                }
            }
            if (Physics.SphereCast(p1, cc.height / 2, -transform.forward, out hit, 1.5f))
            {
                //Debug.DrawRay(transform.position, (cc.transform.position - transform.position), Color.green); // show path of SphereCast
                //Debug.Log("rayHit.collider.tag " + hit.collider.tag); // print collider info

                if (hit.collider.tag == "Enemy")
                {
                    //lower sanity
                    distanceToObstacle = hit.distance;
                    hud.DecSanity(0.4f);
                }
            }
        }

		//end of raycast detection (for sanity)
		*/



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

			if (RotateBook) {
				book.transform.Rotate (0, 0, 35.0f * (Time.deltaTime / 2));
				amountRot += 35.0f * (Time.deltaTime / 2);
				if (amountRot > 35.0f) {
					RotateBook = false;
					moveBookcase = true;
				}
			}

			if (moveBookcase && Bookcase.transform.localPosition.x < 22.6f) {
                bcs.Play();
                Bookcase.transform.Translate (Vector3.back * 1.0f * Time.deltaTime);
				book.transform.Translate (Vector3.back * 1.0f * Time.deltaTime);
				Skulls.transform.Translate (Vector3.down * 4.0f * Time.deltaTime);
                //back(y-), forward(y++), right(x++), left(x--), up??
                //bcs.Play();
			}
			if (Skulls.transform.position.z < 7.0f && Skulls.transform.position.z > -17.0f) {
				Skulls.transform.Translate (Vector3.down * 5.0f * Time.deltaTime);
			}
			

			if (Input.GetButtonDown ("Open")) {
				if ((Vector3.Distance (transform.position, book.transform.position) < 5)) {
					RotateBook = true;
				}

			}

			if (Input.GetButtonDown ("Open")) {
				if ((Vector3.Distance (transform.position, Flashlight.transform.position) < 5)) {
					HasFlashlight = true;
					Flashlight.transform.localPosition = new Vector3 (-100, -100, -100);
					//GUI.Label (Rect(10, 10, 100, 20), "Press M to work the flashlight");			}
				}
			}

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
			} else if (Input.GetKey ("o")) {
				//hud.DecSanity(1);
			} else if (Input.GetKey ("p")) {
				//hud.IncSanity(1);
		} else if (Input.GetButtonDown ("Open")) {
				//press e if object is tagged as door get the object and then call OpenClose function in DoorScript
				Vector3 fwd = transform.TransformDirection (Vector3.forward);
				RaycastHit doorhit;
				if (Physics.Raycast (transform.position, fwd, out doorhit)) {
					if ((transform.position - doorhit.transform.position).magnitude <= 3.0f) {
						if (doorhit.collider.tag == "Door") {
							DoorScript dr = doorhit.transform.gameObject.GetComponent<DoorScript> ();
							if (dr.isopen ()) {
								dr.Close ();
							} else {
								dr.Open ();
							}
						} else if (doorhit.collider.tag == "FrontDoor" && HasFlashlight) {
							DoorScript dr = doorhit.transform.gameObject.GetComponent<DoorScript> ();
							dr.Open ();
							hud.WonGame ();
						}
					}
				}
			}

			speed = new Vector3 ((sideSpeed * 2), verticalVelocity, (forwardSpeed * 2));

			//editing speed to match camera direction
			speed = transform.rotation * speed;


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
