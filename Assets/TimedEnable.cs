using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEnable : MonoBehaviour
{
    public float timeLimit = 5f;
    public GameObject obj;
    private float timer = 0f;

    public void timedEnable()
    {
        timer += Time.deltaTime;
        obj.SetActive(true);
        while(timer < timeLimit) { }
        obj.SetActive(false);
    }
}
