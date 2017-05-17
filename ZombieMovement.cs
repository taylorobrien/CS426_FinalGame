using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
	Animator anim;
	//int walkHash = Animator.StringToHash("Walking");
	//int attackHash = Animator.StringToHash("Attacking");
	GameObject player;
    //int runStateHash = Animator.StringToHash("Base Layer.Run");
    AudioSource zmoan;

	void Start ()
	{
		anim = GetComponent<Animator>();
		player = GameObject.FindWithTag ("MainPlayer");
		anim.Play ("Walking");
		zmoan = GetComponent<AudioSource>();
		StartCoroutine(SoundOut());
    }

    // Update is called once per frame
    void Update()
    {
        if (!HUDScript.isPaused && !HUDScript.isGameOver)
        {
            anim.enabled = true;
            //float move = Input.GetAxis ("Horizontal");
            //anim.SetFloat("Speed", move);
            //anim.SetTrigger (walkHash);

            //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (Vector3.Distance(transform.position, player.transform.position) < 3)
            {
                //anim.SetTrigger (attackHash);//
                anim.Play("Attacking");
            }
            else
            {
                //anim.SetTrigger (walkHash);
                anim.Play("Walking");
            }
        }else
        {
            anim.enabled = false;
        }
    }



    IEnumerator SoundOut()
    {
        while (true)
        {
            zmoan.Play();
            if (HUDScript.isPaused || HUDScript.isGameOver)
            {
                //zmoan.Pause();
            }
            yield return new WaitForSeconds(6);
        }
    }


}

