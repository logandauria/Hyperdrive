using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// Allows the alpha to change on a vfx graph based on beats
public class ParticleColorSync : AudioSyncer
{

	public float beatAlpha = 255;
	public float restAlpha = 0;
	public float triggerTime = 0.2f;
	public float fadeSpeed;
	ParticleSystem vfx = new ParticleSystem();
	// number alpha targeted of the vfx graph... so multiple alpha syncs can be used with different beats
	public float alphaNum = 1;

	private Color[] colors = { Color.red, Color.magenta, Color.green, Color.blue, Color.cyan, Color.white };
	private int selectedColor = 0;

	private float curAlpha = 0;

	void Start()
	{
		vfx = GetComponent<ParticleSystem>();
	}

	private IEnumerator MoveToAlpha()
	{
		curAlpha = vfx.startColor[3];

		float timer = 0;
		// increase the scale based on how much time has passed

		while (timer < triggerTime)
		{
			curAlpha = Mathf.Lerp(curAlpha, beatAlpha, timer / triggerTime);
			timer += Time.deltaTime;

			var col = vfx.colorOverLifetime;
			col.enabled = true;
			Gradient grad = new Gradient();
			grad.SetKeys(new GradientColorKey[] { new GradientColorKey(colors[selectedColor], 0.0f), }, new GradientAlphaKey[] { new GradientAlphaKey(curAlpha, 0.0f) });
			col.color = grad;

			//vfx.startColor = new Vector4(vfx.startColor.r, vfx.startColor.g, vfx.startColor.b, 255);

			yield return null;
		}


		m_isBeat = false;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

		curAlpha = Mathf.Lerp(curAlpha, restAlpha, Time.deltaTime * fadeSpeed);
		//Debug.Log("CurAlpha: " + curAlpha);

		var col = vfx.colorOverLifetime;
		col.enabled = true;
		Gradient grad = new Gradient();
		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(colors[selectedColor], 0.0f), }, new GradientAlphaKey[] { new GradientAlphaKey(curAlpha, 0.0f) });
		col.color = grad;

		// decrease the scale based on how much time has passed
		//vfx.startColor = new Vector4(vfx.startColor.r, vfx.startColor.g, vfx.startColor.b,Mathf.Lerp(curAlpha, restAlpha, Time.deltaTime * fadeSpeed));
		//curAlpha = vfx.startColor[3];
	}

	public override void OnBeat()
	{
		//Debug.Log("colorbeat");
		var col = vfx.colorOverLifetime;
		col.enabled = true;
		Gradient grad = new Gradient();
		selectedColor = Random.Range(0, colors.Length);
		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(colors[selectedColor], 0.0f),}, new GradientAlphaKey[] { new GradientAlphaKey(curAlpha, 0.0f)});
		col.color = grad;
		base.OnBeat();

		StopCoroutine("MoveToAlpha");
		StartCoroutine("MoveToAlpha");
	}
}
