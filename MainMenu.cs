using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject main;

    // SFX
    AudioSource _audio;
    public AudioClip startSFX;
    public AudioClip exitSFX;

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

    public void StartButton()
    {
        OnClickSFX(startSFX);

        // Has to wait to load the game scene, so that the sfx can be played
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.35f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu()
    {
        OnClickSFX(startSFX);

        // Has to wait to switch menus, so that the sfx can be played
        StartCoroutine(SwitchMenu());
    }

    IEnumerator SwitchMenu()
    {
        yield return new WaitForSeconds(0.25f);

        main.SetActive(false);
        options.SetActive(true);
    }

    public void BackButton()
    {
        OnClickSFX(exitSFX);
        options.SetActive(false);
        main.SetActive(true);
    }

    public void ExitGame() 
    {
        OnClickSFX(exitSFX);
        Debug.Log("Exit Game.");
        Application.Quit();
    }

    public void OnClickSFX(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

}
