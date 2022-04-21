using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistornAudioOnLoc : MonoBehaviour
{
    public GameObject toTrack;


    private AudioReverbFilter filter;
    private AudioSource audioSrc;
    private Vector3 initPos;
    public float diff;

    // Start is called before the first frame update
    void Start()
    {
        initPos = toTrack.transform.position;
        filter = GetComponent<AudioReverbFilter>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        diff = initPos.x - toTrack.transform.position.x;
        filter.decayTime = Mathf.Abs(diff) * 500;
        audioSrc.panStereo = diff / 7;
    }
}
