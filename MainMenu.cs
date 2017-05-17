using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public Canvas instructions;
    public Button playbtn;
    public Button exitbtn;
    public Button instructbtn;
    public Button backbtn;

	// Use this for initialization
	void Start () {
        playbtn = playbtn.GetComponent<Button>();
        exitbtn = exitbtn.GetComponent<Button>();
        instructbtn = instructbtn.GetComponent<Button>();
        backbtn = backbtn.GetComponent<Button>();
        instructions = instructions.GetComponent<Canvas>();
        instructions.enabled = false;
        backbtn.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Scene_Level1");
    }

    public void ShowInstructions()
    {
        instructions.enabled = true;
        backbtn.enabled = true;
        playbtn.enabled = false;
        exitbtn.enabled = false;
        instructbtn.enabled = false;
    }

    public void HideInstructions()
    {
        instructions.enabled = false;
        backbtn.enabled = false;
        playbtn.enabled = true;
        exitbtn.enabled = true;
        instructbtn.enabled = true;
    }
}
