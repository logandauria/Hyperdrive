using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// Manages the spawning of roads in the game. Only 2 roads should be currenty active for efficiency
public class RoadSpawner : MonoBehaviour
{
    //public GameObject spawnpoint;
    public float speed;
    // z coordinate where road will despawn
    public float zLimit = -25;
    // offset of first road's spawn position
    public Vector3 offset1;
    // offset of second road's spawn position
    public Vector3 offset2;
    public Vector3 offset3;
    // road prefab to spawn
    public GameObject road;

    // used for automatic spawning together
    public bool useLength;
    public float roadLength;

    // list of active roads in the game
    private List<GameObject> active = new List<GameObject>();

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //active.Add(Instantiate(road, road.transform.position + offset1, road.transform.rotation));
        //active[active.Count - 1].tag = "env";
        //active[active.Count - 1].SetActive(true);
        //SpawnAnother(offset2);
        //SpawnAnother(offset3);
    }

    /// <summary>
    /// Spawn another road at the start or when a road is despawned
    /// </summary>
    void SpawnAnother(Vector3 inputOffset)
    {

        // instantiate
        active.Add(Instantiate(road, road.transform.position + inputOffset, road.transform.rotation));
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
    /// When enabled, spawn 2 roads
    /// </summary>
    void OnEnable()
    {
        active.Add(Instantiate(road, road.transform.position + offset1, road.transform.rotation));
        active[active.Count - 1].tag = "env";
        active[active.Count - 1].SetActive(true);
        SpawnAnother(offset2);
        SpawnAnother(offset3);
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
            //Debug.Log("destroyed");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // move objects and kill objects that pass the z limit
        for (int x = 0; x < active.Count; x++)
        {


            active[x].transform.position = new Vector3(active[x].transform.position.x, active[x].transform.position.y, active[x].transform.position.z - (speed * GlobalSpeed.multiplier));
            if (active[x].transform.position.z < zLimit)
            {
                Destroy(active[x]);
                active.RemoveAt(x);
                x--;
                SpawnAnother(offset3);
            }
        }
    }
}
