using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    // vehicle object that will respond to the rotation
    public GameObject toTrack;
    public float rotation_cap;
    public float multiplier = 1;
    public float rotate_speed = 0.5f;
    public float rotate;

    // Update is called once per frame
    void Update()
    {
        rotate = toTrack.transform.eulerAngles.x * -multiplier;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotate, transform.eulerAngles.z);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rotate, transform.eulerAngles.z), Time.deltaTime * rotate_speed);

        
    }
}
