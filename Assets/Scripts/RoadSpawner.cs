using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class RoadSpawner : MonoBehaviour
{
    //public GameObject spawnpoint;
    public float speed;
    public float zLimit = -25;
    public Vector3 offset1;
    public Vector3 offset2;
    public GameObject road;


    private List<GameObject> active = new List<GameObject>();

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //active.Add(Instantiate(road, road.transform.position + offset1, road.transform.rotation));
        //active[active.Count - 1].tag = "env";
        //active[active.Count - 1].SetActive(true);
        //SpawnAnother();
    }

    void SpawnAnother()
    {

        // instantiate
        active.Add(Instantiate(road, road.transform.position + offset2, road.transform.rotation));
        // make sure to update tag for runtime movement called by other script
        active[active.Count - 1].tag = "env";
        active[active.Count - 1].SetActive(true);
    }

    void OnDisable()
    {
        killAll();
    }

    void OnEnable()
    {
        active.Add(Instantiate(road, road.transform.position + offset1, road.transform.rotation));
        active[active.Count - 1].tag = "env";
        active[active.Count - 1].SetActive(true);
        SpawnAnother();
    }

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
                SpawnAnother();
            }
        }
    }
}
