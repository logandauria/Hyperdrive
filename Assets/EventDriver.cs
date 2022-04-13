using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDriver : MonoBehaviour
{
    public GameObject toToggle;
    public float toggleLength = 10;

    public GameObject[] scenes;
    private int counter = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

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
