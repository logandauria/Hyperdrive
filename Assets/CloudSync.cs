using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;


public class CloudSync : AudioSyncer
{
	private Volume volume;
	private VolumeProfile volumeProfile;
	private VolumetricClouds clouds;
	//private var clouds;

	public float beatShape;
	public float restShape;
	public float beatErosion;
	public float restErosion;
	public float triggerTime;
	public bool randomRange;

	private float timer = 0;

	public void Start()
    {
		volume = GetComponent<Volume>();
		volumeProfile = volume.sharedProfile;
		if (!volumeProfile.TryGet<VolumetricClouds>(out clouds))
		{
			clouds = volumeProfile.Add<VolumetricClouds>(false);
		}

		clouds.verticalShapeWindSpeed.overrideState = true;
		clouds.verticalErosionWindSpeed.overrideState = true;

		//clouds.enabled.overrideState = true;

	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat)
		{
			timer += Time.deltaTime;
			if (timer > triggerTime)
			{
				m_isBeat = false;
				timer = 0;
			}

			return;
		}

		// return values to initial state
		clouds.verticalShapeWindSpeed.value = restShape;
		clouds.verticalErosionWindSpeed.value = restErosion;
	}

	public override void OnBeat()
	{
		base.OnBeat();

		clouds.verticalShapeWindSpeed.value = beatShape;
		clouds.verticalErosionWindSpeed.value = beatErosion;

		// select a random range between desired values
		/*if (randomRange)
		{
			vfx.SetFloat("intensity", Random.Range(restVector.x, beatVector.x));
			vfx.SetFloat("drag", Random.Range(restVector.y, beatVector.y));
			vfx.SetFloat("frequency", Random.Range(restVector.z, beatVector.z));
		}
		// set specific values provided in the inspector
		else
		{
			vfx.SetFloat("intensity", beatVector.x);
			vfx.SetFloat("drag", beatVector.y);
			vfx.SetFloat("frequency", beatVector.z);
		}*/
	}
}
