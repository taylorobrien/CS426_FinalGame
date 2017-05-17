using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Won : MonoBehaviour {

    public Button returnbtn;
    public Button exitbtn;

    // Use this for initialization
    void Awake () {
        returnbtn = returnbtn.GetComponent<Button>();
        exitbtn = exitbtn.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Return()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
