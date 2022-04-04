using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // what gameojbect will be instantiated
    public GameObject spawnObject;
    // possible spawn positions (going to need more for assets appearing on the road)
    public Vector3 spawnPos1;
    public Vector3 spawnPos2;
    // how fast objects travel
    public float speed;
    // z float specifying how far the objects will travel
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
        // move the objects z position by the speed variable
        objects[1].transform.position = new Vector3(objects[1].transform.position.x, objects[1].transform.position.y, objects[1].transform.position.z - speed);
        objects[0].transform.position = new Vector3(objects[0].transform.position.x, objects[0].transform.position.y, objects[0].transform.position.z - speed);

        // Destroy objects if they pass the kill coordinate position
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
