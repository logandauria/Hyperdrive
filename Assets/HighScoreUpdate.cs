using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Create a faux MPH reading based on the global speed variable
public class HighScoreUpdate : MonoBehaviour
{
    // text UI to change
    public TextMeshProUGUI curr;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void DisplayScore()
    {
        curr.text = "High Score: " + (int)((GlobalSpeed.highScore * 10) / 2) + " mph";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
