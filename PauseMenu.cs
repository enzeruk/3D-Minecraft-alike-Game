using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void ResumeGame()
    {
        //SceneManager.UnloadScene("Pause");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game.");
        Application.Quit();
    }

}
