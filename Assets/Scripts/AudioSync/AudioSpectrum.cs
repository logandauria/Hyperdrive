using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// engine for analyzing spectrum data
/// </summary>
public class AudioSpectrum : MonoBehaviour {

	private void Update()
    {
        // get the data
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        // assign spectrum value
        // this "engine" focuses on the simplicity of other classes only..
        // ..needing to retrieve one value (spectrumValue)
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }

    private void Start()
    {
        m_audioSpectrum = new float[128];
    }

    // AudioSyncer is given this for beat extraction
    public static float spectrumValue {get; private set;}

    // Spectrum data from Unity
    private float[] m_audioSpectrum;

}
