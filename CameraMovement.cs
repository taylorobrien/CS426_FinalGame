using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMovement : MonoBehaviour {
    Camera c;
	//CharacterController cc;
	// Use this for initialization
    void Awake()
    {
        c = GetComponent<Camera>();
		//cc = GetComponent<CharacterController> ();
    }
	void Start () {
        /*For som reason, the camera drops 0.5 units upon start...?*/
		//Camera.main.transform.position = new Vector3 (0, 2.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = new Vector3 (0, Camera.main.transform.position.y, 0);
        if (!HUDScript.isPaused && !HUDScript.isGameOver)
        {
			//move camera up and down with mouse movement
			float rotateUpDown = Input.GetAxis ("Mouse Y");
			//Camera c = GetComponent<Camera> ();
			c.transform.Rotate (-rotateUpDown, 0, 0);

		} 
		if (Input.GetButtonDown ("Crouch")) {
			Camera.main.transform.localPosition = new Vector3 (0, 1.5f, 0);
			//transform.position. = new Vector3 (0, 1f, 0);


		} else if (Input.GetButtonUp ("Crouch")) {
			
			Ray crouchRay = new Ray(transform.position, transform.up);
			RaycastHit crouchHit;

			if (Physics.Raycast (crouchRay, out crouchHit, 2.0f)) {
				//Debug.Log ("Hit");
				//Debug.DrawLine (crouchRay.origin, crouchHit.point, Color.green);
				Camera.main.transform.localPosition = new Vector3 (0, 1.5f, 0);
			} else {
				Camera.main.transform.localPosition = new Vector3 (0, 2.0f, 0);
			}


//			
		}


	
    }
}
