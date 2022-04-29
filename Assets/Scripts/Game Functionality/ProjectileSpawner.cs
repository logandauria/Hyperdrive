using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// Used to spawn projectiles in the road that move towards the player
public class ProjectileSpawner : MonoBehaviour
{
    // Game objects where the projectile will spawn at
    public GameObject[] spawnpoints;
    // if true, spawn object somewhere random between two points/object positions in the spawnpoints array
    public bool spawnPointsAreRange = false;
    // speed of projectile
    public float speed;
    // time required between projectile spawns
    public float spawnTime = 1;
    // z coordinate where projectiles will despawn
    public float zLimit = 0;
    // offset applied to the spawn location
    public Vector3 offset;
    // projectile prefab list
    public GameObject[] prefabs;

    // stores the projectiles that are active in the scene
    private List<GameObject> active = new List<GameObject>();

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Spawn a new projectile on a random spawn point and initialize all its necessary values
    /// </summary>
    void Spawn()
    {
        // select random prefab and random lane of the lists provided in the inspector
        int select = Random.Range(0, prefabs.Length - 1);
        int lane = Random.Range(0, spawnpoints.Length);
        // if true, spawn object somewhere random between the two points
        if (spawnPointsAreRange)
        {
            Vector3 pos = new Vector3(Random.Range(spawnpoints[0].transform.position.x, spawnpoints[spawnpoints.Length - 1].transform.position.x), spawnpoints[0].transform.position.y, spawnpoints[0].transform.position.z);
            active.Add(Instantiate(prefabs[select], pos + offset, spawnpoints[lane].transform.rotation));
        }
        else
        {
            // instantiate
            active.Add(Instantiate(prefabs[select], spawnpoints[lane].transform.position + offset, spawnpoints[lane].transform.rotation));
        }
        // make sure to update tag for runtime movement called by other script
        active[active.Count - 1].tag = "env";
        
        
        active[active.Count - 1].SetActive(true);
    }

    /// <summary>
    /// When attached game object disabled, kill all active objects
    /// </summary>
    void OnDisable()
    {
        killAll();
    }

    /// <summary>
    /// kill all active objects in the active list
    /// </summary>
    public void killAll()
    {
        Debug.Log("kill all");
        for (int x = 0; x < active.Count; x++)
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
        if (timer > spawnTime / GlobalSpeed.multiplier)
        {
            timer = 0;
            Spawn();
        }

        // move objects and kill objects that pass the z limit
        for (int x = 0; x < active.Count; x++)
        {
            active[x].transform.position = new Vector3(active[x].transform.position.x, active[x].transform.position.y, active[x].transform.position.z - (speed * GlobalSpeed.multiplier));
            if (active[x].transform.position.z < zLimit)
            {
                Destroy(active[x]);
                active.RemoveAt(x);
                x--;
            }
        }
    }
}
