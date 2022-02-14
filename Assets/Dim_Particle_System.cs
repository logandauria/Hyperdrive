using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dim_Particle_System : MonoBehaviour
{

    public GameObject toTrack;
    public ParticleSystem yourParticle;
    public float emission;
    public float multiplier;
    public float dim;

    // Update is called once per frame
    void Update()
    {
        dim = toTrack.transform.eulerAngles.z;
        dim = (dim > 180) ? dim - 360 : dim;
        if(dim < 0)
        {
            dim *= -1;
        }
        dim = dim * multiplier;

        var ParticleEmission = yourParticle.emission;
        ParticleEmission.enabled = true;
        ParticleEmission.rateOverTime = emission - dim;

    }
}
