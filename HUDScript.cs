using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour {

    public Canvas hudcanvas;
    public Slider SanitySlider;
    public Button continuebtn;
    public Button quitbtn;
    public Button restartbtn;
    public float maxSanity = 100.0f;
    public float currSanity;
    public static bool isPaused = false;
    public static bool isGameOver = false;
    Animator anim;


    // Use this for initialization
    void Awake()
    {
        isPaused = false;
        isGameOver = false;
        hudcanvas = hudcanvas.GetComponent<Canvas>();
        SanitySlider = hudcanvas.GetComponentInChildren<Slider>();
        continuebtn = hudcanvas.GetComponentInChildren<Button>(); ;
        quitbtn = hudcanvas.GetComponentInChildren<Button>();
        restartbtn = hudcanvas.GetComponentInChildren<Button>();
        currSanity = maxSanity;
        continuebtn.enabled = false;
        quitbtn.enabled = false;
        restartbtn.enabled = false;
        anim = GetComponent<Animator>();
    }

	void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if(currSanity == 0)
        {
            GameOver();
        }
    }

    public void DecSanity(float x)
    {
        currSanity -= x;
        if (currSanity < 0.0f)
        {
            currSanity = 0.0f;
        }
        SanitySlider.value = currSanity;
    }

    public void IncSanity(float x)
    {
        currSanity += x;
        if (currSanity > maxSanity)
        {
            currSanity = maxSanity;
        }
        SanitySlider.value = currSanity;
    }

    public void PauseGame()
    {
        //Debug.Log("Here1");
        //yield return new WaitUntil(() => anim.isInitialized);
        //There is an error here because it seems to trying to 
        //call the animator before the animatior is fully initialized.
        //Try the yield code above?
        anim.SetTrigger("PauseGameTrig");
        isPaused = true;
        isGameOver = false;
        //Screen.lockCursor = false; //OBSOLETE
        Cursor.lockState = CursorLockMode.None;
        continuebtn.enabled = true;
        quitbtn.enabled = true;
        GetComponent<AudioSource>().Pause();
    }

    public void ContinueGame()
    {
        //Debug.Log("Here2");
        anim.SetTrigger("UnpauseTrig");
        isPaused = false;
        isGameOver = false;
        //Screen.lockCursor = true; //OBSOLETE
        Cursor.lockState = CursorLockMode.Locked;
        continuebtn.enabled = false;
        quitbtn.enabled = false;
        restartbtn.enabled = false;
        GetComponent<AudioSource>().Play();
    }

    public void QuitGame()
    {
        isPaused = false;
        isGameOver = false;
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void RestartLevel()
    {
        isPaused = false;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        anim.SetTrigger("GameOverTrig");
        isGameOver = true;
        isPaused = false;
        //Screen.lockCursor = false; //OBSOLETE
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
        quitbtn.enabled = true;
        restartbtn.enabled = true;
    }

    public void WonGame()
    {
        anim.SetTrigger("WonTrig");
        isPaused = true;
        isGameOver = false;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(gamewait());
        //SceneManager.LoadScene("Scene_GameWon");
    }

    IEnumerator gamewait()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scene_GameWon");
    }
}
