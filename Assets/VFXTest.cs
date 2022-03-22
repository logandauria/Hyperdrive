using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class VFXTest : AudioSyncer
{

	VisualEffect vfx = new VisualEffect();

	private IEnumerator MoveToScale(Vector3 _target)
	{
		Vector3 _curr = new Vector3(vfx.GetFloat("intensity"), vfx.GetFloat("drag"), vfx.GetFloat("frequency"));
		Vector3 _initial = _curr;
		float _timer = 0;

		while (_curr != _target)
		{
			_curr = Vector3.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;

			vfx.SetFloat("intensity", _curr.x);
			vfx.SetFloat("drag", _curr.y);
			vfx.SetFloat("frequency", _curr.z);


			yield return null;
		}

		m_isBeat = false;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

		Vector3 _curr = new Vector3(vfx.GetFloat("intensity"), vfx.GetFloat("drag"), vfx.GetFloat("frequency"));
		Vector3 back = Vector3.Lerp(_curr, restScale, restSmoothTime * Time.deltaTime);
		vfx.SetFloat("intensity", back.x);
		vfx.SetFloat("drag", back.y);
		vfx.SetFloat("frequency", back.z);
	}

	public override void OnBeat()
	{
		base.OnBeat();

		StopCoroutine("MoveToScale");
		StartCoroutine("MoveToScale", beatScale);
	}

	public void Start()
    {
		vfx = GetComponent<VisualEffect>();
    }

	public Vector3 beatScale;
	public Vector3 restScale;
}
