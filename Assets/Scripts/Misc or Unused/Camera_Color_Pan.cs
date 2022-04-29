using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// outdated for HDRP
// change the background color of the camera according to the steering wheel's z rotation
public class Camera_Color_Pan : MonoBehaviour
{
    public GameObject toTrack;
    public Camera cam;
    public float pan;
    public float multiplier;

    // Update is called once per frame
    void Update()
    {
        pan = toTrack.transform.eulerAngles.z;
        pan = (pan > 180) ? pan - 360 : pan;
        // normalize pan to scale since desired values are in range -1 to 1
        pan /= 160;
        pan = Mathf.Abs(pan);
        pan *= multiplier;
        cam.backgroundColor = new Color(pan * 2, pan * .5f, 0,1);
        cam.backgroundColor = new Color(pan * .5f, pan * .7f, pan * .9f, 1);

    }
}
