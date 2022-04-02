using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track_Rotation : MonoBehaviour
{

    public GameObject toTrack;
    public float multiplier;
    public float rotation_cap;
    public float rotate_speed;
    public float rotate;

    // Update is called once per frame
    void Update()
    {
        rotate = toTrack.transform.eulerAngles.x;
        rotate = (rotate > 180) ? rotate - 360 : rotate;
        rotate *= -multiplier;
        if (rotate > rotation_cap)
        {
            rotate = rotation_cap;
        } else if (rotate < -rotation_cap)
        {
            rotate = -rotation_cap;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rotate, transform.eulerAngles.z), Time.deltaTime * rotate_speed);

    }
}
