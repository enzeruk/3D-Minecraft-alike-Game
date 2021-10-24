using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeGameGUI : MonoBehaviour
{

    public static int life, points;
    public Text myText;

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
        myText.text = " ♥ " + life + "\nScore: " + points;
    }

}
