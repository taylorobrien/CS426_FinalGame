using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_1 : MonoBehaviour {
	NavMeshAgent agent;
	public HUDScript hud;
	GameObject mainPlayer;
	Vector3 target;
	Vector3 returnPosition;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		mainPlayer = GameObject.FindWithTag ("MainPlayer");
		hud = GameObject.FindObjectOfType(typeof(HUDScript)) as HUDScript;
		returnPosition = agent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!HUDScript.isPaused && !HUDScript.isGameOver) {
			{
				if (mainPlayer.transform.localPosition.z > -9.29f && mainPlayer.transform.localPosition.z < 10.13f && mainPlayer.transform.localPosition.x > 8.01f) {//z between -9.29 and 10.13			 x greater than 8.01
					agent.speed = 2.0f;
					agent.SetDestination (mainPlayer.transform.position);
					if (Vector3.Distance (transform.position, mainPlayer.transform.position) < 3) {
						hud.DecSanity (0.2f);
					}

				} else if (agent.transform.localPosition.x != 8.97f && agent.transform.localPosition.z != -7.09f) { //position	{(12.2, 0.1, -6.1)}	UnityEngine.Vector3
					agent.speed = 2.0f;
					//target = new Vector3 (/8.97f, agent.transform.localPosition.y, -7.09f);
					agent.SetDestination (returnPosition);
				} else {
					agent.speed = 0.0f;
					agent.transform.Rotate (new Vector3 (0, 0, 0));

				}
			}
		}
		else{
			agent.speed = 0.0f;
		}
	
	}	
}
