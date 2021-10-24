using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public GameObject UIGamePaused;
    public string hitTransformBefore;

    // Reference to GameController
    public static GameController gc;

    // Set things up here
    void Awake()
    {
        // Setup reference to GameController so that the methods in this script can be called by other scripts
        if (gc == null)
            gc = this.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
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
            if (--GUISetUp.life < 0)
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

    // Pause the game by pressing the ESCAPE button
    void PauseGame()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        if (Input.GetKeyDown("p"))
        {
            if (Time.timeScale > 0f)
            {
                UIGamePaused.SetActive(true); // this brings up the pause UI
                Time.timeScale = 0f; // this pauses the game action
            }
            else
            {
                Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
                UIGamePaused.SetActive(false); // remove the pause UI
            }
        }
    }

}
