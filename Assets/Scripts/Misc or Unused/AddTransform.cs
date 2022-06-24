using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTransform : MonoBehaviour
{

    public GameObject toTrack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = toTrack.transform.localPosition;
        this.transform.localEulerAngles = toTrack.transform.localEulerAngles;   
    }
}
