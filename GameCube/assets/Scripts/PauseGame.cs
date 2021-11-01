using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PauseGame : MonoBehaviour
{

    public static bool isPaused;

    public GameObject UIGamePaused;
    public GameObject touchControls;

    // SFX
    public AudioClip resumeSFX;
    public AudioClip exitSFX;
    AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        {
            // If AudioSource is missing
            Debug.LogWarning("AudioSource component is missing.");
            // Add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))                     // for pc
        if (CrossPlatformInputManager.GetButtonDown("Settings"))    // for mobile
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PlaySFX(exitSFX);
                Pause();
            }
        }
    }

    // Pause the game by pressing the ESCAPE button
    void Pause()
    {
        Debug.Log("Game Paused.");
        
        UIGamePaused.SetActive(true); // this brings up the pause UI
        Time.timeScale = 0f; // this pauses the game action  

        // Disable the FPS script to unlock the cursor for the Game Over menu scene
        touchControls.SetActive(false);

        isPaused = true;
    }

    public void Resume()
    {
        Debug.Log("Resume Game.");

        PlaySFX(resumeSFX);
        UIGamePaused.SetActive(false); // remove the pause UI
        Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)  

        // Enable the Touchpad so that the user can rotate the main camera view while in Game Scene
        touchControls.SetActive(true);

        isPaused = false;
    }

    public void MainMenu()
    {
        PlaySFX(exitSFX);
        Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
                             // Disable the FPS script to unlock the cursor for the Game Over menu scene
        enableFPS(false);   // Unlock the cursor that has been locked due to FPS' script
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        PlaySFX(exitSFX);
        Debug.Log("Exit Game.");
        Application.Quit();
    }

    void PlaySFX(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    void enableFPS(bool enable)
    {
        GameObject fpsObj = GameObject.Find("FPSController");
        FirstPersonController fpsScript = fpsObj.GetComponent<FirstPersonController>();

        if (enable)
        {
            //Enable FPS script
            fpsScript.enabled = true;
        }
        else
        {
            //Disable FPS script
            fpsScript.enabled = false;
            //Unlock Mouse and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
