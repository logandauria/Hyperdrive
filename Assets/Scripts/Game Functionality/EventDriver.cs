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
    // actual portal
    public GameObject portal;
    // Enemy manager
    public GameObject enemyManager;

    // VFX for portal burst transition, attached to portalBurst object
    private VisualEffect portalBurstVFX;

    private VisualEffect portalVFX;
    // track the initial emission for reversion since we have to set it to zero
    private float initBurstEmission;

    // int to store current level
    private int counter = 0;



    // Start is called before the first frame update
    void Start()
    {
        portalVFX = portal.GetComponent<VisualEffect>();
        portalBurstVFX = portalBurst.GetComponent<VisualEffect>();
        initBurstEmission = portalBurstVFX.GetFloat("emission");
    }

    void youLose()
    {
        GlobalSpeed.multiplier = 0;
        Debug.Log("YOU LOSE");
    }

    /// <summary>
    /// Called through a unity event to change to the next level
    /// </summary>
    public void nextScene()
    {
        enemyManager.SetActive(false);
        if (counter == scenes.Length - 1)
        {
            youLose();
        }
        else
        {
            // activate quick portal burst for seamless transition
            portalBurst.SetActive(true);
            portalBurstVFX.SetFloat("emission", 50000);
            portalVFX.SetFloat("emission", 50000);


            // turn off current scene and increment to next after delay
            Invoke("IncrementScene", 2f);

            // turn on next scene after delay
            
            Invoke("UpdateScene", 5f);
            Invoke("FadePortal", 6f);
        }
        
    }

    public void FadePortal()
    {
        portalVFX.SetFloat("emission", 0);
    }



    public void IncrementScene()
    {
        scenes[counter].SetActive(false);
        counter += 1;
        counter %= scenes.Length;
        // activate actual portal
        portalScene.SetActive(true);

        // turn off emission for portal burst
        portalBurstVFX.SetFloat("emission", 0f);

    }

    public void UpdateScene()
    {
        portalScene.SetActive(false);
        scenes[counter].SetActive(true);

        // deactivate portal burst
        portalBurst.SetActive(false);
        enemyManager.SetActive(true);
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
