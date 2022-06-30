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

    private Color[] predefinedColors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
