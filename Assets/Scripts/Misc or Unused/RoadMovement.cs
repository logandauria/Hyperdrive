using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control the movement of roads towards the player
public class RoadMovement : MonoBehaviour
{
    // z coordinate where road will despawn
    public float ZLimit;

    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        if(transform.position.z < ZLimit)
        {
            this.transform.position = initPos;
        }
    }
}
