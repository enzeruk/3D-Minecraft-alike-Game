using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    public float endYPos;
    public bool fallDetected = false;

    // Update is called once per frame
    void Update()
    {
        FallDamage();
    }

    // Player's fall damage
    void FallDamage()
    {
        if (!GameObject.FindObjectOfType<CharacterController>().isGrounded)
        {
            fallDetected = true;

            Transform playerTrans = gameObject.GetComponent<Transform>();
            endYPos = (int)playerTrans.transform.position.y;

            if (UserInputSetting.n != 0 && endYPos < 0)
            {
                Debug.Log("You died.");
                if (GameController.gc)             // if GameController is available then loseLife() 
                    GameController.gc.loseLife();

                // Teleport the player the the begining (magenta cube position)
                playerTrans.root.position = new Vector3(UserInputSetting.n / 2, UserInputSetting.n, UserInputSetting.n / 2);
                Debug.Log("Teleported to the start.");
            }

        }

        else if (GameObject.FindObjectOfType<CharacterController>().isGrounded)
        {
            Transform playerTrans = gameObject.GetComponent<Transform>();

            if (fallDetected)
            {
                endYPos = (int)playerTrans.transform.position.y - 1;

                // The player fell to the exact previous level 
                if (endYPos == PlayerJump.startYPos)
                {
                    PlayerJump.startYPos--; // falls 1 level
                }

                // The player fell more levels than 1
                else if (endYPos < PlayerJump.startYPos)
                {
                    // Change the value of startYPos with the previous value of the endYPos,
                    // because the player didnt jump so the startYPos won't change to the level that it should
                    PlayerJump.startYPos = PlayerJump.startYPos + 1;

                    CubeManagement.reduseSc = (int)(PlayerJump.startYPos - endYPos);

                    // Lose 10 points for each level the player falls
                    Debug.Log("You fall.");

                    if (GameController.gc)             // if GameController is available then subScore() 
                        GameController.gc.subScore((10 * CubeManagement.reduseSc) - 10);

                    // Now the startYPos will be the endYPos (the level that the player already fell to)
                    PlayerJump.startYPos = endYPos - 1;    // -1 because the statement's algorithm adds 1 to startYPos each time 
                }

                fallDetected = false;

            }

        }
    }

}
