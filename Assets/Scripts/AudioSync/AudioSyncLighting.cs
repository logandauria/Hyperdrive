using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioSyncLighting : AudioSyncer
{
	public float beatIntensity;
	public float restIntensity;
	public bool setColor;
	public Color inputColor;
	private Image m_img;

	private Light light;

	private IEnumerator ScaleLighting(float _target)
	{
		float _curr = light.intensity;
		float _initial = _curr;
		float _timer = 0;

		while (_curr < _target)
		{
			_curr = _initial + (_target - _initial) * (_timer / timeToBeat);
			_timer += Time.deltaTime;

			light.intensity = _curr;

			// check if editor defines color
			if (setColor)
			{
				light.color = inputColor;
			}
			else
			{
				light.color = m_img.color;
			}

			yield return null;
		}

		m_isBeat = false;
	}

	public override void OnUpdate()
	{
		base.OnUpdate();

		if (m_isBeat) return;

		light.intensity = light.intensity + (restIntensity - light.intensity) * (restSmoothTime * Time.deltaTime);

		// check if editor defines color
		if (setColor)
		{
			light.color = inputColor;
		}
		else
		{
			light.color = m_img.color;
		}

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
		m_img = GetComponent<Image>();
	}
}