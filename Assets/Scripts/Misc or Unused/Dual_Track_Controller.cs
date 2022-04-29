using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dual_Track_Controller : MonoBehaviour
{
    public GameObject toTrack;
    public AudioSource audio1;
    public AudioSource audio2;
    public float pan;

    // Update is called once per frame
    void Update()
    {
        pan = toTrack.transform.eulerAngles.z;
        pan = (pan > 180) ? pan - 360 : pan;
        // normalize pan to scale since desired values are in range -1 to 1
        pan /= 160;
        pan = Mathf.Abs(pan);
        audio1.volume = 1 - pan;
        audio2.volume = 1 - audio1.volume - 0.1f;
    }
}
