using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class EnemyProperties : MonoBehaviour
{

    public UnityEvent youLose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("YOU LOSE!");
        GlobalSpeed.multiplier = 0;

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
