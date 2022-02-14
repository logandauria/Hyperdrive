using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Controller : MonoBehaviour
{
    public GameObject toTrack;
    public float pan;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        pan = toTrack.transform.eulerAngles.z;
        pan = (pan > 180) ? pan - 360 : pan;
        // normalize pan to scale since desired values are in range -1 to 1
        pan /= 160;

        audio.panStereo = pan;
    }
}
