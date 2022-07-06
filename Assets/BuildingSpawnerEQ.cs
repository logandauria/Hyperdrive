using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

// Spawns buildings by instantiating prefabs that will move towards the player until the z limit
public class BuildingSpawnerEQ : MonoBehaviour
{
    // INSPECTOR VALUES
    // Game objects that the buildings can spawn at
    public GameObject[] spawnpoints;
    // speed the buildings will move
    public float speed;
    // time in between spawns
    public float spawnTime = 1;
    // z coordinate limit where the buildings will despawn
    public float zLimit = 0;
    // position offset of where buildings are spawned
    public Vector3 offset;
    // whether the spawned objects will have a random rotation
    public bool randomRotation = false;
    // whether the spawned objects will have a random scale
    public bool randomScale = false;
    // whether the prefab objects contain VFX graphs
    // list of objects to spawn
    public GameObject[] prefabs;
    

    // active objects in the scene
    private List<GameObject> active = new List<GameObject>();

    private float timer = 0;

    private VisualEffect vfx = new VisualEffect();

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Spawn a new object on a random spawn point and initialize all its necessary values
    /// </summary>
    void Spawn()
    {
        // select random prefab and random lane of the lists provided in the inspector
        
        GameObject select = prefabs[Random.Range(0, prefabs.Length - 1)];

        AudioSyncScale auss = select.GetComponent<AudioSyncScale>();
        auss.bias = Random.Range(5, 40);
        auss.beatScale += new Vector3(0, Random.Range(0, 5), 0);

        int lane = Random.Range(0, spawnpoints.Length);
        // instantiate
        active.Add(Instantiate(select, spawnpoints[lane].transform.position + offset, spawnpoints[lane].transform.rotation));
        // make sure to update tag for runtime movement called by other script
        active[active.Count - 1].tag = "env";
        // customize prefab:
        if (randomRotation) active[active.Count - 1].transform.eulerAngles += new Vector3(0, Random.Range(0, 360), 0);

        if (randomScale)
        {
            // apply random scale
            float scaleInc = Random.Range(-0.2f, 1.5f);
            active[active.Count - 1].transform.localScale += new Vector3(scaleInc, scaleInc, scaleInc);
        }
        // customize prefab:
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
        if (timer > spawnTime)
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
