using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Controls the environment/level changes of the game
public class EventDriver : MonoBehaviour
{
    // GameObject to toggle
    public GameObject toToggle;
    public float toggleLength = 10;

    // List containing all of the scene objects
    public GameObject[] scenes;
    // int to store current level
    private int counter = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Called through a unity event to change to the next level
    /// </summary>
    public void nextScene()
    {
        scenes[counter].SetActive(false);
        counter += 1;
        counter %= scenes.Length;
        scenes[counter].SetActive(true);
    }

    

    // Update is called once per frame
    void Update()
    {
        /*toToggle.SetActive(true);

        if (timer > toggleLength)
        {
            toToggle.SetActive(false);
            timerOn = false;
            maxHit = false;
            doOnce = true;
        }*/
    }
}
