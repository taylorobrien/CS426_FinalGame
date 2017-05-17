using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GhostMovement : MonoBehaviour {
	public Transform playerpos;
	NavMeshAgent agent;
    public Transform target;
	public HUDScript hud;
	GameObject mainPlayer;
	float x;
	float y;
	float z;

    bool chasing = false;

	void Awake()
	{
		//hud = GameObject.GetComponent<HUDScript>();
		hud = GameObject.FindObjectOfType(typeof(HUDScript)) as HUDScript;
		//m_someOtherScriptOnAnotherGameObject.Test();
		agent = GetComponent<NavMeshAgent> ();
		mainPlayer = GameObject.FindWithTag ("MainPlayer");
		//agent.
    }


    void Update(){
		//transform.position.Set (transform.position.x, -1.37f, transform.position.z);
        if (!HUDScript.isPaused && !HUDScript.isGameOver){
            agent.speed = 2.0f;
            agent.SetDestination(target.position);
            if (!chasing){
                if ((agent.transform.position - target.position).magnitude <= 3.0f){
                    Vector3 rnddir = Random.insideUnitSphere * 10;
                    rnddir += transform.position;
                    NavMeshHit meshhit;
                    NavMesh.SamplePosition(rnddir, out meshhit, 10, 1);
                    target.position = new Vector3(meshhit.position.x, transform.position.y, meshhit.position.z);
                    agent.SetDestination(target.position);
                }
            }else if (chasing){
                if ((agent.transform.position - target.position).magnitude <= 2.5f){
                    agent.speed = 0;
                }else{
                    agent.speed = 2.0f;
                }
            }
            //chases the player so long as player is directly in front of it
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
			//increase y position to the transform.position and place that in Raycast
            if (Physics.Raycast(transform.position, fwd, out hit)){
                if (hit.transform == playerpos){
                    chasing = true;
                    target.position = new Vector3(playerpos.position.x, playerpos.position.y, playerpos.position.z);
                    agent.SetDestination(target.position);
                }else{
                    chasing = false;
                }
            }
            if (Vector3.Distance(transform.position, mainPlayer.transform.position) < 3)
            {
                hud.DecSanity(0.2f);
            }
        }
        else{
            agent.speed = 0;
        }
	}
}
	
