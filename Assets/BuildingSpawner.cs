using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] spawnpoints;
    public float speed;
    public float spawnTime = 1;
    public float zLimit = 0;
    public Vector3 offset;
    public GameObject[] prefabs;

    private List<GameObject> active = new List<GameObject>();

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Spawn()
    {
        // select random prefab and random lane of the lists provided in the inspector
        int select = Random.Range(0, prefabs.Length - 1);
        int lane = Random.Range(0, spawnpoints.Length);
        // instantiate
        active.Add(Instantiate(prefabs[select], spawnpoints[lane].transform.position + offset, spawnpoints[lane].transform.rotation));
        // customize prefab:
        active[active.Count - 1].SetActive(true);
    }

    void OnDisable()
    {
        killAll();
    }

    public void killAll()
    {
        Debug.Log("kill all");
        for(int x = 0; x < active.Count; x++)
        {
            Destroy(active[x]);
            active.RemoveAt(x);
            x--;
        }
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
