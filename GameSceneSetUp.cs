using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSetUp : MonoBehaviour
{

    public GameObject[] CubePrefab;
    public GameObject MagentaCube;
    public GameObject TransparentCube;
    public GameObject SpotLightPrefab;
    public GameObject FirstPersonController;

    // Reference to GamseSceneSetUp
    public static GameSceneSetUp gmSetUp;

    // Set things up here
    void Start()
    {
        // Generate the game scene
        CreateGameScene();
    }

    public void CreateGameScene()
    {
        Transform playerTransform = FirstPersonController.GetComponent<Transform>();

        // Create the Level 0 (floor) with cubes of random color
        System.Random rnd = new System.Random();
        int number;

        for (int i = 1; i < UserInputSetting.n + 1; i++)
        {
            for (int j = 1; j < UserInputSetting.n + 1; j++)
            {
                // Place the magenta colored cube in the middle of the scene
                if (i == UserInputSetting.n / 2 && j == UserInputSetting.n / 2)
                {
                    Instantiate(MagentaCube, new Vector3(i, 1, j), Quaternion.identity);
                }
                else
                {
                    number = rnd.Next(5);
                    Debug.Log("number = " + number);
                    Instantiate(CubePrefab[number], new Vector3(i, 1, j), Quaternion.identity);
                }

            }

        }

        // Player must not get out of the NxN plane
        // Creation of NxN wall from transparent cubes around the scene
        for (int i = 0; i < UserInputSetting.n + 2; i++)
        {
            for (int j = 0; j < UserInputSetting.n + 2; j++)
            {
                if (i == 0 || j == 0 || i == UserInputSetting.n + 1 || j == UserInputSetting.n + 1)
                {
                    Instantiate(TransparentCube, new Vector3(i, 1, j), Quaternion.identity);
                }
            }
        }

        // Instantiate 2nd spotlight on opposite end of grid
        GameObject spotLight = Instantiate(SpotLightPrefab, new Vector3(UserInputSetting.n, 4, UserInputSetting.n), Quaternion.identity);

        spotLight.transform.Rotate(Vector3.right, 150);
        spotLight.transform.Rotate(Vector3.up, -45);


        // Initialize player's position on magenta cube (the middle of the scene)
        playerTransform.position = new Vector3(UserInputSetting.n / 2, 3, UserInputSetting.n / 2);

    }

}
