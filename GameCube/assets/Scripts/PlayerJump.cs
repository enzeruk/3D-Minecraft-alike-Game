using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public static float startYPos;

    AudioSource _audio;
    public AudioClip victorySFX;

    void Start()
    {
        startYPos = 1;      // Player starts @ Level#1

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
        DoJump();
    }

    // Player jumps on a cube/cylinder, gets 10 points
    public void DoJump()
    {
        if (Input.GetKeyDown("space"))
        {
            if (GUISetUp.life > 0)
            {
                Transform playerTrans = gameObject.GetComponent<Transform>();
                float y_ang = playerTrans.rotation.eulerAngles.y;

                // Check if cube/cylinder exists in front of player
                Vector3 newPos, playerPos;

                // Player's chessboard coordinates
                playerPos = new Vector3((int)(playerTrans.position.x + 0.5f),
                                        (int)playerTrans.position.y + 1,
                                        (int)(playerTrans.position.z + 0.5f));

                if (y_ang >= 0.0f && y_ang <= 45.0f || y_ang >= 315.0f && y_ang <= 360.0f)
                {

                    // Place a cube where it should be placed
                    newPos = new Vector3((int)(playerTrans.position.x + 0.5f),
                                         (int)playerTrans.position.y - 1,
                                        ((int)(playerTrans.position.z + 0.5f)) + 1);

                }

                else if (y_ang >= 45.0f && y_ang <= 90.0f || y_ang >= 90.0f && y_ang <= 135.0f)
                {
                    newPos = new Vector3(((int)(playerTrans.position.x + 0.5f)) + 1,
                                          (int)playerTrans.position.y - 1,
                                          (int)(playerTrans.position.z + 0.5f));

                }

                else if (y_ang >= 135.0f && y_ang <= 180.0f || y_ang >= 180.0f && y_ang <= 225.0f)
                {
                    newPos = new Vector3((int)(playerTrans.position.x + 0.5f),
                                          (int)playerTrans.position.y - 1,
                                         ((int)(playerTrans.position.z + 0.5f)) - 1);

                }

                else if (y_ang >= 225.0f && y_ang <= 270.0f || y_ang >= 270.0f && y_ang <= 315.0f)
                {
                    newPos = new Vector3(((int)(playerTrans.position.x + 0.5f)) - 1,
                                          (int)playerTrans.position.y - 1,
                                          (int)(playerTrans.position.z + 0.5f));

                }

                else
                {
                    // Needed for compiler, no actual use
                    newPos = new Vector3(((int)(playerTrans.position.x + 0.5f)),
                                          (int)playerTrans.position.y - 1,
                                          (int)(playerTrans.position.z + 0.5f));

                }

                // Returns array of colliders colliding with checkSphere
                Collider[] hitColliders = Physics.OverlapSphere(newPos, 0.2f);

                // There exists an object in front of the player (called jump shape)
                if (hitColliders.Length > 0)
                {
                    // Jumping on cube
                    if (hitColliders[0].name.Contains("Cube"))
                    {
                        // Get each time the player's height position for fall check later
                        startYPos = (int)gameObject.transform.position.y - 1;

                        // Position above jump cube
                        Vector3 auxPos = new Vector3(newPos.x, newPos.y + 1, newPos.z);

                        // Check for object on top of the cube which the player will jump on
                        if (Physics.CheckSphere(auxPos, 0.2f))
                        {
                            Debug.Log("Stacked object found.");
                            return;
                        }

                        auxPos.y++;

                        // Check for obstacle above the cube which the player will jump on
                        if (Physics.CheckSphere(auxPos, 0.2f))
                        {
                            Debug.Log("Not enough space for player to fit.");
                            return;
                        }

                        // Check for obstacle above player
                        if (Physics.CheckSphere(playerPos, 0.2f))
                        {
                            Debug.Log("Obstacle on top of player, player can't jump.");
                            return;
                        }

                        // Teleport on top of this cube
                        playerTrans.root.position = new Vector3(newPos.x, newPos.y + 2, newPos.z);

                        // Jumping on top of cube with N - 1 height places player at N-th Level
                        if (newPos.y - 1 == (UserInputSetting.n - 1))
                        {
                            // Play SFX
                            _audio.PlayOneShot(victorySFX);

                            Debug.Log("Reached the top height!");
                            if (GameController.gc)                  // if GameController is available then gainLife() and addScore()     
                            {
                                GameController.gc.gainLife();
                                GameController.gc.addScore(100);
                            }      
                        }

                        else
                        {
                            if (GameController.gc)                  // if GameController is available then addScore()
                                GameController.gc.addScore(10);
                        }

                    }

                    else if (hitColliders[0].name.Contains("Cylinder"))
                    {
                        // Get each time the player's height position for fall check later
                        startYPos = (int)gameObject.transform.position.y;

                        // Position above jump cylinder
                        Vector3 auxPos = new Vector3(newPos.x, newPos.y + 2, newPos.z);

                        // Check for object on top of the cylinder which the player will jump on
                        if (Physics.CheckSphere(auxPos, 0.2f))
                        {
                            Debug.Log("Stacked object found.");
                            return;
                        }

                        auxPos.y++;

                        // Check for obstacle above the cylinder which the player will jump on
                        if (Physics.CheckSphere(auxPos, 0.2f))
                        {
                            Debug.Log("Not enough space for player to fit.");
                            return;
                        }

                        // Check for obstacle above player
                        if (Physics.CheckSphere(playerPos, 0.2f))
                        {
                            Debug.Log("Obstacle on top of player, player can't jump.");
                            return;
                        }

                        int newY = (int)newPos.y + 3;

                        // Teleport on top of this cylinder
                        playerTrans.root.position = new Vector3(newPos.x, newY, newPos.z);

                        // Jumping on top of cylinder with N - 1 height places player at N-th Level
                        if (newPos.y == (UserInputSetting.n - 1))
                        {

                            // Play SFX
                            _audio.PlayOneShot(victorySFX);

                            Debug.Log("Reached the top height!");
                            if (GameController.gc)                  // if GameController is available then gainLife() and addScore()     
                            {
                                GameController.gc.gainLife();
                                GameController.gc.addScore(100);
                            }
                        }

                        else
                        {
                            if (GameController .gc)                 // if GameController is available then addScore()
                                GameController.gc.addScore(10);
                        }

                    }

                }

                else
                {
                    // No object at location
                    Debug.Log("No object in front of player to jump on.");
                }
            }

        }
    }

}
