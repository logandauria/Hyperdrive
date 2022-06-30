using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to create an accessible cityColor attribute for other scripts
// to use for syncing color of multiple VFX/particles
public class AudioColorPick : AudioSyncer
{
    // whether or not to use random colors
    public bool randomColors = true;
    public static Color cityColor;

    private Color[] predefinedColors = { 
        new Color(1,0,0,1),
        new Color(1,1,0,1),
        new Color(1,1,1,1),
        new Color(1,0,1,1),
        new Color(0,0,1,1),
        new Color(0,1,1,1),
        new Color(0,1,0,1)
    };


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WTF");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnBeat()
    {
        base.OnBeat();
        Debug.Log("new color " + AudioColorPick.cityColor);

        if (randomColors)
        {
            cityColor = new Color(Random.Range(.5f, 1f), Random.Range(.5f, 1f), Random.Range(.5f, 1f), 1);

        }
        else
        {
            cityColor = predefinedColors[Random.Range(0, predefinedColors.Length - 1)];
        }
    }

}

