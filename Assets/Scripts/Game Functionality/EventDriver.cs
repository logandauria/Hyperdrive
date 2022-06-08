using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

// Controls the environment/level changes of the game
public class EventDriver : MonoBehaviour
{
    // GameObject to toggle
    public GameObject toToggle;
    public float toggleLength = 10;

    // List containing all of the scene objects
    public GameObject[] scenes;
    // Scene for portal transition
    public GameObject portalScene;
    // Object for portal burst transition
    public GameObject portalBurst;


    // VFX for portal burst transition, attached to portalBurst object
    private VisualEffect portalBurstVFX;
    // track the initial emission for reversion since we have to set it to zero
    private float initBurstEmission;

    // int to store current level
    private int counter = 0;



    // Start is called before the first frame update
    void Start()
    {
        portalBurstVFX = portalBurst.GetComponent<VisualEffect>();
        initBurstEmission = portalBurstVFX.GetFloat("emission");
    }

    /// <summary>
    /// Called through a unity event to change to the next level
    /// </summary>
    public void nextScene()
    {
        // activate quick portal burst for seamless transition
        portalBurst.SetActive(true);
        portalBurstVFX.SetFloat("emission", initBurstEmission);

        // turn off current scene and increment to next after delay
        Invoke("IncrementScene", 1f);

        // activate actual portal
        portalScene.SetActive(true);

        // turn off emission for portal burst
        portalBurstVFX.SetFloat("emission", 0f);

        // turn on next scene after delay
        Invoke("UpdateScene", 5f);

        // deactivate portal burst
        portalBurst.SetActive(false);
    }

    public void IncrementScene()
    {
        scenes[counter].SetActive(false);
        counter += 1;
        counter %= scenes.Length;
    }

    public void UpdateScene()
    {
        portalScene.SetActive(false);
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
