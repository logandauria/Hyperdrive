using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 position;
    public float speed;
    public float speedMultiplier;

    public bool hitPlayer = false;
    bool scored = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        transform.position = position + new Vector3(0,0,-1);

        if(transform.position.z < -10 && !scored && !hitPlayer)
        {
            EnemyManager.AvoidedCar();
            scored = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit by car");
        if (collision.gameObject.tag == "Player")
        {
            hitPlayer = true;
            EnemyManager.HitByCar();
        }

    }
}
