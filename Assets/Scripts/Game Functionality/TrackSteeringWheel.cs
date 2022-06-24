using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSteeringWheel : MonoBehaviour
{

    public GameObject steeringWheel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, steeringWheel.transform.localEulerAngles.x);
    }
}
