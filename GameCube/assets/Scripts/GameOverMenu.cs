using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // SFX
    AudioSource _audio;
    public AudioClip gameOverSFX;
    public AudioClip startSFX;
    public AudioClip exitSFX;

    // Start is called before the first frame update
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        {
            // If AudioSource is missing
            Debug.LogWarning("AudioSource component is missing.");
            // Add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }

        OnClickSFX(gameOverSFX);
    }

    // Update is called once per frame
    public void MainMenu()
    {
        OnClickSFX(startSFX);

        // Has to wait to load the Main Menu scene, so that the sfx can be played
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.35f);

        // Reload the Main Menu scene
        SceneManager.LoadScene("Menu");
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
