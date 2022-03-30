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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Spawn()
    {
        GameObject obj1 = Instantiate(spawnObject, spawnPos1);
        GameObject obj2 = Instantiate(spawnObject, spawnPos2);
    }

    // Update is called once per frame
    void Update()
    {
        obj1.transform.position = new Vector3(obj1.transform.position.x, obj1.transform.position.y, obj1.transform.position.z - speed);
        obj2.transform.position = new Vector3(obj2.transform.position.x, obj2.transform.position.y, obj2.transform.position.z - speed);

        if (obj1.transform.position.z < killCoord)
        {
            Destroy(obj1);
        }
        if (obj2.transform.position.z < killCoord)
        {
            Destroy(obj2);
        }
    }
}
