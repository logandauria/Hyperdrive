using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AudioGroupedVFXColorSync : AudioSyncer
{


	public VisualEffect[] vfx;
	private float timer = 0;
	public float triggerTime = .2f;

	private float[] blendPercents;

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat)
		{
			for (int i = 0; i < vfx.Length; i++)
			{
				vfx[i].SetFloat("blend1", Mathf.Lerp(vfx[i].GetFloat("blend1"), blendPercents[i], Time.deltaTime / timeToBeat));
			}


			timer += Time.deltaTime;
			if (timer > triggerTime)
			{
				m_isBeat = false;
				timer = 0;
			}

			return;
		}
		foreach (VisualEffect v in vfx)
		{
			v.SetFloat("blend1", Mathf.Lerp(v.GetFloat("blend1"), 0, Time.deltaTime));
		}
	}

	public override void OnBeat()
	{
		base.OnBeat();
		// Create random blend percentages for each building so they are not all the same shade
		for (int i = 0; i < blendPercents.Length; i++)
		{
			blendPercents[i] = Random.Range(0.15f, 1f);
        }
		foreach (VisualEffect v in vfx)
		{
			v.SetVector4("color1", AudioColorPick.cityColor);
		}
	}


	// Start is called before the first frame update
	void Start()
	{
		blendPercents = new float[vfx.Length];
	}
}