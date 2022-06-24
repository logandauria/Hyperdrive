using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectOffset : MonoBehaviour
{

    public GameObject toTrack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = toTrack.transform.position;
    }
}
