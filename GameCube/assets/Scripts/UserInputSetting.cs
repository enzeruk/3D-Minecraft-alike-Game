/* Computer Graphics and Interactive Systems 2019   *
 *      Angeliki Kalliri     A.M. 2446              */

// TODO : LoadGameScene() @ line 50

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserInputSetting : MonoBehaviour
{
    
    public GameObject UI;
    public static int n;       // User's input (NxNxN dimensions of the scene) 

    void LoadGameScene()
    {    
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void InitializeScene(string userInput)
    { 
        UI.SetActive(false);

        // User's input N
        if (Int32.TryParse(userInput, out n))
            Debug.Log(n + "x" + n + " scene was made.");
        else
        {
            Debug.Log("String could not be parsed.");
            return;
        }

        LoadGameScene();
    }
}
