using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

// Controls the collision functionality of an enemy
public class EnemyProperties : MonoBehaviour
{
    // Unity event to trigger when a collision occurs
    public UnityEvent collideEvent;

    public GameObject particleCar;


    private VisualEffect particleCarVFX;
    private Collider meshCol;
    private Renderer meshRen;

    // prevent collisions from spamming
    private float colTimer = 0f;
    private float colTimeLimit = 1f;
    private bool triggerOnceEverySec = true;

    // Start is called before the first frame update
    void Start()
    {
        // obtain the collider of the enemy
        meshCol = GetComponent<Collider>();
        // obtain the renderer of the enemy
        meshRen = GetComponent<Renderer>();

        particleCarVFX = particleCar.GetComponent<VisualEffect>();

        if(meshRen == null)
        {
            meshRen = GetComponentInChildren<Renderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggerOnceEverySec)
        {
            colTimer += Time.deltaTime;
            if(colTimer > colTimeLimit)
            {
                triggerOnceEverySec = true;
                colTimer = 0f;
            }
        }
    }


    /// <summary>
    /// Called when an enemy is collided with
    /// </summary>
    void OnCollisionEnter(Collision col)
    {
        if (triggerOnceEverySec)
        {
            triggerOnceEverySec = false;
            colTimer = 0f;
            Debug.Log("COLLISION!");

            meshCol.enabled = false;
            meshRen.enabled = false;
            //Debug.Log("YOU LOSE!");
            particleCar.SetActive(true);
            particleCar.transform.position = this.transform.position;
            particleCarVFX.SetFloat("emission", 0);

            collideEvent.Invoke();
            Invoke("resetParticleCar", 2);

            if (col.gameObject.tag != "Player")
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), col.gameObject.GetComponent<Collider>(), true);

            }
            else
            {

            }
        }
    }


    void resetParticleCar()
    {
        particleCar.SetActive(false);

        particleCarVFX.SetFloat("emission", 50000);

    }
}
