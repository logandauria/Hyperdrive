using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXSyncColorBlend : AudioSyncer
{

    float colorBlendPercent = 0f;
    public float triggerTime = .2f;
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
