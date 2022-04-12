using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VFXSyncYVelocity : AudioSyncer
{

	VisualEffect vfx = new VisualEffect();

	public float restVelocity;
	public float beatVelocity;
	public float triggerTime;

	private float timer = 0;


	public override void OnUpdate()
	{
		base.OnUpdate();
		timer += Time.deltaTime;
		if(timer > triggerTime)
        {
			vfx.SetFloat("yvel", restVelocity);
			Debug.Log("low");
			timer = 0;
		}
	}

	public override void OnBeat()
	{
		base.OnBeat();
		vfx.SetFloat("yvel", beatVelocity);
	}

	public void Start()
	{
		vfx = GetComponent<VisualEffect>();
	}
}
