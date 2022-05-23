using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because all environments' objects move towards the player, to increase the speed of the game we need to increase the speed
// of all of those objects. This is a class meant to represent a global speed multiplier that can be accessed by all scripts
// to maintain a consistent speed increase.
public class GlobalSpeed : MonoBehaviour
{
    // global speed multiplier for use in other classes
    [SerializeField]
    public static float multiplier = 2f;
    // how much to increment the multiplier every frame
    public float speedInc = .0003f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        multiplier += speedInc;
    }
}
