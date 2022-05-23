using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosLerp : MonoBehaviour
{

    // inspector values
    public GameObject[] objectPositions;
    public GameObject lookAt;
    public Vector3 lookAtOffset;
    public bool runOffTime = false;
    public float timeToChange = 2f;
    public float moveSpeed = 1f;

    private int lastPos = 0;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ChangePos()
    {
        int rand = lastPos;
        if (objectPositions.Length > 1)
        {
            // dont go to duplicate object
            while (rand == lastPos)
            {
                 rand = Random.Range(0, objectPositions.Length - 1);
            }
            lastPos = rand;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // slowly move to selected objects position while looking at object
        transform.position = Vector3.Lerp(transform.position, objectPositions[lastPos].transform.position, Time.deltaTime * moveSpeed);
        transform.LookAt(lookAt.transform.position + lookAtOffset);

        if (runOffTime)
        {
            timer += Time.deltaTime;
            if (timer > timeToChange)
            {
                timer = 0;
                ChangePos();
            }
        }
    }
}
