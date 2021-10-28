using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class GameController : MonoBehaviour
{
    public string hitTransformBefore;

    // Reference to GameController
    public static GameController gc;

    //public GameObject UIGamePaused;

    // Set things up here
    void Awake()
    {
        //UIGamePaused.SetActive(false); // remove the pause UI

        // Setup reference to GameController so that the methods in this script can be called by other scripts
        if (gc == null)
            gc = this.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOverScene();
    }

    // Gain points
    public void addScore(int gainedPoints)
    {
        GUISetUp.points += gainedPoints;
        Debug.Log("You gain " + gainedPoints + " points!");
    }

    // Lose points
    public void subScore(int lostPoints)
    {
        if ((GUISetUp.points - lostPoints) < 0)
        {
            if (GUISetUp.life < 0)
            {
                Debug.Log("Game over.");
            }

            //points = 100 + (points - lostPoints); // Parenthesis will be negative
        }
        else
        {
            GUISetUp.points -= lostPoints;
            Debug.Log("You lose " + lostPoints + " points.");
        }

    }

    // Gain a life 
    public void gainLife()
    {
        if (GUISetUp.life > 0)
        {
            GUISetUp.life++;
            Debug.Log("You gain 1 life!");
        }

        else
            Debug.Log("Game over.");

    }

    // Lose a life
    public void loseLife()
    {
        if (GUISetUp.life > 0)
        {
            GUISetUp.life--;
            Debug.Log("You lose 1 life.");
        }
        else
            Debug.Log("Game over.");

    }

    void GameOverScene()
    {
        if (GUISetUp.life == 0)
        {
            // Disable the FPS script to unlock the cursor for the Game Over menu scene
            enableFPS(false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
