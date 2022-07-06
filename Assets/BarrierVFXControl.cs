using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Updates the barrier VFX attributes based on its distance to the player (XR Origin)
/// </summary>
public class BarrierVFXControl : MonoBehaviour
{
    // XR origin for calculating distance
    public GameObject origin;

    public float triggerDist = 2f;

    private float dist = 0f;
    private VisualEffect vfx;

    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Mathf.Abs(origin.transform.position.x - transform.position.x);
        if (dist < triggerDist)
        {
            float factor = triggerDist - dist;
            vfx.SetFloat("emission", Mathf.Pow(100, factor));
            vfx.SetFloat("yvel", factor * 2);
        }
        else
        {
            vfx.SetFloat("emission", 0);
        }
    }
}
