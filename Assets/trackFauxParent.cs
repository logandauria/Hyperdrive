using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is needed for objects that need to track their original parents that change due to the XRGrabInteractable script
public class trackFauxParent : MonoBehaviour
{
    // desired fake parent
    public GameObject fauxParent;
    // whether or not to track the rotations for xyz
    public bool trackXRot;
    public bool trackYRot;
    public bool trackZRot;

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = fauxParent.transform.localPosition;

        this.transform.localEulerAngles = new Vector3(trackXRot ? fauxParent.transform.localEulerAngles.x : this.transform.localEulerAngles.x,
                                                 trackYRot ? fauxParent.transform.localEulerAngles.y : this.transform.localEulerAngles.y,
                                                 trackZRot ? fauxParent.transform.localEulerAngles.z : this.transform.localEulerAngles.z);
    }
}
