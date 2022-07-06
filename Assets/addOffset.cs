using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addOffset : MonoBehaviour
{

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
