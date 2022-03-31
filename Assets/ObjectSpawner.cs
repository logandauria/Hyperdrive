using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject spawnObject;
    public Vector3 spawnPos1;
    public Vector3 spawnPos2;
    public float speed;
    public float killCoord = -10;

    private GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = new GameObject[2];
    }

    void Spawn()
    {
        //objects[0] = Instantiate(spawnObject, spawnPos1);
        //objects[1] = Instantiate(spawnObject, spawnPos2);
    }

    // Update is called once per frame
    void Update()
    {
        objects[1].transform.position = new Vector3(objects[1].transform.position.x, objects[1].transform.position.y, objects[1].transform.position.z - speed);
        objects[0].transform.position = new Vector3(objects[0].transform.position.x, objects[0].transform.position.y, objects[0].transform.position.z - speed);

        if (objects[0].transform.position.z < killCoord)
        {
            Destroy(objects[1]);
        }
        if (objects[1].transform.position.z < killCoord)
        {
            Destroy(objects[0]);
        }
    }
}
