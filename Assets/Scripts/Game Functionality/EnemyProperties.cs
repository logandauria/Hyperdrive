using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

// Controls the collision functionality of an enemy
public class EnemyProperties : MonoBehaviour
{
    // Unity event to trigger when a collision occurs
    public UnityEvent youLose;

    private Collider meshCol;
    private Renderer meshRen;

    // Start is called before the first frame update
    void Start()
    {
        meshCol = GetComponent<Collider>();
        meshRen = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    /// <summary>
    /// Called when an enemy is collided with
    /// </summary>
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("YOU LOSE!");
        GlobalSpeed.multiplier = 0;

        meshCol.enabled = false;
        meshRen.enabled = false;

        if (col.gameObject.tag != "Player")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), col.gameObject.GetComponent<Collider>(), true);

        }
        else
        {
            GlobalSpeed.multiplier = 0;
            //Debug.Log("YOU LOSE!");

            youLose.Invoke();
        }
    }
}
