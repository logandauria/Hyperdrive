using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VFXTest2 : AudioSyncer
{

	VisualEffect vfx = new VisualEffect();

	public Vector3 beatVector;
	public Vector3 restVector;
	public float triggerTime;
	public bool randomRange;

	private float timer = 0;

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat)
		{
			timer += Time.deltaTime;
			if (timer > triggerTime) {
				m_isBeat = false;
				timer = 0;
			}

			return;
		}

		vfx.SetFloat("intensity", restVector.x);
		vfx.SetFloat("drag", restVector.y);
		vfx.SetFloat("frequency", restVector.z);
	}

	public override void OnBeat()
	{
		base.OnBeat();

		if (randomRange)
		{
			vfx.SetFloat("intensity", Random.Range(restVector.x, beatVector.x));
			vfx.SetFloat("drag", Random.Range(restVector.x, beatVector.x));
			vfx.SetFloat("frequency", Random.Range(restVector.x, beatVector.x));
		}
		else
		{
			vfx.SetFloat("intensity", beatVector.x);
			vfx.SetFloat("drag", beatVector.y);
			vfx.SetFloat("frequency", beatVector.z);
		}
	}

	public void Start()
	{
		vfx = GetComponent<VisualEffect>();
	}

}
