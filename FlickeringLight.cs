using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FlickeringLight : MonoBehaviour {

	float minFlickerSpeed  = 0.1f;
	float maxFlickerSpeed  = 1.0f;
	Light myLight;
	float timeOn;
	float timeOff;
	private float changeTime = 0;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light>();
		timeOn = 0.1f;
		timeOff = 0.5f;
	}




	void Update() {
		timeOn = Random.Range (minFlickerSpeed, maxFlickerSpeed);
		timeOff = Random.Range (minFlickerSpeed, maxFlickerSpeed);
		if (Time.time > changeTime) {
			myLight.enabled = !myLight.enabled;
			if (myLight.enabled) {
				changeTime = Time.time + timeOn;
			} else {
				changeTime = Time.time + timeOff;
			}
		}
	}
		
}
