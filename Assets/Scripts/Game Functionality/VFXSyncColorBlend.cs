using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Blends a color into a VFX on audio beats. Currently only accesses 'blend1' variable of a VFX graph
public class VFXSyncColorBlend : AudioSyncer
{
	// time the blend will trigger for
    public float triggerTime = .2f;
	// maximum blend percentage (0-1) can go over 1 for faster blend
	public float blendPercent = .8f;

	private float timer = 0f;
	private VisualEffect vfx = new VisualEffect();

	void Start()
    {
		vfx = GetComponent<VisualEffect>();
    }

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat)
		{
			vfx.SetFloat("blend1", Mathf.Lerp(vfx.GetFloat("blend1"), blendPercent, Time.deltaTime));



			timer += Time.deltaTime;
			if (timer > triggerTime)
			{
				m_isBeat = false;
				timer = 0;
			}

			return;
		}

		vfx.SetFloat("blend1", Mathf.Lerp(vfx.GetFloat("blend1"), 0, Time.deltaTime));
	}

}
