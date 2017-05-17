using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    Animator anim;
    bool open = false;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open()
    {
        anim.SetTrigger("DoorOpenTrig");
        open = true;
        GetComponent<AudioSource>().Play();
    }

    public void Close()
    {
        anim.SetTrigger("DoorCloseTrig");
        open = false;
        GetComponent<AudioSource>().Play();
    }

    public bool isopen()
    {
        return open;
    }

}
