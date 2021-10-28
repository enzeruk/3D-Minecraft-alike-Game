using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISetUp : MonoBehaviour
{

    public static int life, points;
    public Text mainText;
    public Text rsrvText;
    public Text objText;
    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        life = 4;
        points = 100;
    }

    // Update is called once per frame
    void Update()
    {
        InitializeGUI();
    }

    void InitializeGUI()
    {
        // Display Life and Points
        mainText.text = " ♥ " + life + "\nScore: " + points;

        // Cube and cylinder reserve is being calculated @ CubeManagement script
        rsrvText.text = CubeManagement.cube_rsrv + " cube(s) left.\n" + CubeManagement.cylinder_rsrv +" cylinder(s) left.";

        objText.text = "OBJECTIVE: Use the cubes of the floor to reach Level #" + UserInputSetting.n + " in height!";
        levelText.text = "Current level #" + PlayerJump.startYPos;
    }

}
