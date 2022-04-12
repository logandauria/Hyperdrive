using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] spawnpoints;
    public float speed;
    public float spawnTime = 1;
    public float zLimit = 0;
    public GameObject[] prefabs;

    public List<GameObject> active = new List<GameObject>();

    private float offset = 0f;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Spawn()
    {
        // select random prefab and random lane of the lists provided in the inspector
        int select = Random.Range(0, prefabs.Length - 1);
        int lane = Random.Range(0, spawnpoints.Length - 1);
        // instantiate
        active.Add(Instantiate(prefabs[select], spawnpoints[lane].transform.position, spawnpoints[lane].transform.rotation));
        // customize prefab:
        //active[active.Count-1]
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            timer = 0;
            Spawn();
        }

        // kill objects that pass the z limit
        foreach(GameObject g in active)
        {
            g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z - speed);
            if (g.transform.position.z < zLimit)
            {
                Destroy(g);
                active.Remove(g);
            }
        }
    }
}
