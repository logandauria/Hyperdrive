using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSyncLighting : AudioSyncer
{
	public float beatIntensity;
	public float restIntensity;
	public bool setColor;
	public Color beatColor;
	private Color initColor;
	private Light light;

	private IEnumerator ScaleLighting(float _target)
	{
		float _currIntensity = light.intensity;
		float _initial = _currIntensity;
		float _timer = 0;

		Color _currCol = light.color;
		Color _initialCol = _currCol;

		while (_currIntensity < _target)
		{
			_timer += Time.deltaTime;

			// update intensity
			_currIntensity = _initial + (_target - _initial) * (_timer / timeToBeat);
			light.intensity = _currIntensity;
			// update color
			light.color = Color.Lerp(_initialCol, beatColor, _timer / timeToBeat);

			yield return null;
		}

		m_isBeat = false;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

		// Go back to initial values
		light.intensity = light.intensity + (restIntensity - light.intensity) * (restSmoothTime * Time.deltaTime);

		light.color = Color.Lerp(light.color, initColor, restSmoothTime * Time.deltaTime);


	}

	public override void OnBeat()
	{
		base.OnBeat();

		StopCoroutine("ScaleLighting");
		StartCoroutine("ScaleLighting", beatIntensity);
	}


	// Start is called before the first frame update
	void Start()
    {
		light = GetComponent<Light>();
		initColor = light.color;
	}
}