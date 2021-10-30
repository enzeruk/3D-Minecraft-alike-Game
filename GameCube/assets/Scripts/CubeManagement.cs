using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CubeManagement : MonoBehaviour
{

    public string hitTransformBefore;
    public GameObject[] CubePrefab, CylinderPrefab;
    public static int cube_rsrv, cylinder_rsrv;
    public static int reduseSc;

    // SFX
    public AudioClip getSFX;
    public AudioClip placeSFX;
    AudioSource _audio;

    void Start()
    {
        cube_rsrv = 0;
        cylinder_rsrv = 0;
        reduseSc = 0;

        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        { 
            // If AudioSource is missing
            Debug.LogWarning("AudioSource component is missing.");
            // Add the AudioSource component dynamically
            _audio = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        GetCube();
        PlaceCube();
        PlaceCylinder();
    }

    // Get reserve from the cubes by pressing the E key
    public void GetCube()
    {
        //if (Input.GetKeyDown("e"))                            // for pc
        if (CrossPlatformInputManager.GetButtonDown("Get"))     // for mobile
        {
            if (GUISetUp.life > 0 && GUISetUp.points > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5))
                {
                    //Visible only on Scene Mode
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 2.5f);

                    if (hit.transform.name == "GreenCube(Clone)")
                    {
                        // Keep the position of the green cube, destroy it and place a red one 
                        Vector3 cube_position;
                        cube_position = new Vector3(hit.transform.position.x,
                                                    hit.transform.position.y,
                                                    hit.transform.position.z);

                        Destroy(hit.transform.gameObject);
                        Instantiate(CubePrefab[3], cube_position, Quaternion.identity);

                        cube_rsrv++;

                        // Play SFX
                        _audio.PlayOneShot(getSFX);

                        Debug.Log("Cube reserved +1!");
                        if (GameController.gc)              // if GameController is available then subScore()
                            GameController.gc.subScore(5);  

                    }

                    else if (hit.transform.name == "RedCube(Clone)")
                    {
                        // Keep the position of the red cube, destroy it and place a yellow one 
                        Vector3 cube_position;
                        cube_position = new Vector3(hit.transform.position.x,
                                                    hit.transform.position.y,
                                                    hit.transform.position.z);

                        Destroy(hit.transform.gameObject);
                        Instantiate(CubePrefab[4], cube_position, Quaternion.identity);

                        cube_rsrv++;

                        // Play SFX
                        _audio.PlayOneShot(getSFX);

                        Debug.Log("Cube reserved +1!");
                        if (GameController.gc)
                            GameController.gc.subScore(5);

                    }

                    else if (hit.transform.name == "YellowCube(Clone)")
                    {
                        // Keep the position of the yellow cube, destroy it and place a blue one 
                        Vector3 cube_position;
                        cube_position = new Vector3(hit.transform.position.x,
                                                    hit.transform.position.y,
                                                    hit.transform.position.z);

                        Destroy(hit.transform.gameObject);
                        Instantiate(CubePrefab[0], cube_position, Quaternion.identity);

                        cube_rsrv++;

                        // Play SFX
                        _audio.PlayOneShot(getSFX);

                        Debug.Log("Cube reserved +1!");
                        if (GameController.gc)              // if GameController is available then subScore()
                            GameController.gc.subScore(5);

                    }

                    else if (hit.transform.name == "CyanCube(Clone)")
                    {
                        // Destroy the cyan cube and get a cyan cylinder                    
                        Destroy(hit.transform.gameObject);

                        cylinder_rsrv++;

                        // Play SFX
                        _audio.PlayOneShot(getSFX);

                        Debug.Log("Cylinder reserved +1!");
                        if (GameController.gc)              // if GameController is available then subScore()
                            GameController.gc.subScore(5);
                    }

                }

                if (hit.transform != null)
                    hitTransformBefore = hit.transform.name;
            }

            else
            {
                Debug.Log("Not enough points.");
                if (GameController.gc)              // if GameController is available then loseLife()
                    GameController.gc.loseLife();
            }
        }
    }

    // Place a cube in the scene by pressing the LMB, if the cube's reserve is valid
    void PlaceCube()
    {
        //if (Input.GetMouseButtonDown(0))                          // for pc   
        if (CrossPlatformInputManager.GetButtonDown("PlaceCube"))   // for mobile
        {
            if (GUISetUp.life > 0)
            {
                // If there is no reserve
                if (cube_rsrv < 1)
                {
                    Debug.Log("Not enough reserve for cube.");
                    return;
                }

                // Player's transform
                Transform playerTrans = gameObject.GetComponent<Transform>();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5))
                {
                    // Place the cube according to the rotation of the player
                    float y_ang = playerTrans.rotation.eulerAngles.y;

                    Vector3 newPos;

                    if (y_ang >= 0.0f && y_ang <= 45.0f || y_ang >= 315.0f && y_ang <= 360.0f)
                    {
                        // Place the cube where it should be placed
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


                    // Place a cube upon a cube
                    // Start by checking the Level
                    int level = ((int)playerTrans.position.y) - 1;

                    // Find first empty level, starting from player's level
                    while (Physics.CheckSphere(newPos, 0.2f) && level <= UserInputSetting.n)
                    {
                        level++;
                        newPos.y = level;
                    }

                    if (level <= UserInputSetting.n)
                    {
                        newPos.y = level;
                        Instantiate(CubePrefab[Random.Range(0, CubePrefab.Length)], newPos, Quaternion.identity);
                        cube_rsrv--;                                // decrease the reserve by 1

                        // Play SFX
                        _audio.PlayOneShot(placeSFX);

                        Debug.Log("Cube reserved -1!");
                        if (GameController.gc)                      // if GameController is available then addScore()
                            GameController.gc.addScore(10);         // gain 10 points for placing a cube
                    }

                }
            }

        }
    }

    // Place a cylinder in the scene by pressing the C key, if the cylinder's reserve is valid
    void PlaceCylinder()
    {
        //if (Input.GetKeyDown("c"))                                    // for pc
        if (CrossPlatformInputManager.GetButtonDown("PlaceCylinder"))   // for mobile
        {
            if (GUISetUp.life > 0)
            {
                // If there is no reserve
                if (cylinder_rsrv < 1)
                {
                    Debug.Log("Not enough reserve for cylinder.");
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 50))
                {
                    Transform playerTrans = gameObject.GetComponent<Transform>();
                    float y_ang = playerTrans.rotation.eulerAngles.y;

                    Vector3 newPos;

                    if (y_ang >= 0.0f && y_ang <= 45.0f || y_ang >= 315.0f && y_ang <= 360.0f)
                    {
                        // Place the cube where it should be placed
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


                    // Place the cylinder
                    // Start by checking the level
                    int level = ((int)playerTrans.position.y) - 1;

                    // Checks upper part of cylinder
                    Vector3 auxPos = new Vector3(newPos.x, newPos.y + 1, newPos.z);

                    // Find first empty level, starting from player's level
                    while ((Physics.CheckSphere(newPos, 0.2f) ||
                             Physics.CheckSphere(auxPos, 0.2f)) && level <= UserInputSetting.n)
                    {
                        newPos.y = ++level;
                        auxPos.y = level + 1;

                    }

                    if (level <= UserInputSetting.n)
                    {
                        newPos.y = level + 0.5f;            // place cylider at the right height
                        Instantiate(CylinderPrefab[0], newPos, Quaternion.identity);
                        cylinder_rsrv--;                    // decrease the reserve by 1

                        // Play SFX
                        _audio.PlayOneShot(placeSFX);

                        Debug.Log("Cylinder reserved -1!");
                        if (GameController.gc)                  // if GameController is available then addScore()
                            GameController.gc.addScore(20);     // gain 20 points
                    }

                }
            }

        }
    }

}
