using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;


public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] spawnpoints;
    public float speed;
    public float spawnTime = 1;
    public float zLimit = 0;
    public Vector3 offset;
    public bool randomRotation = false;
    public bool randomScale = false;
    public bool isVFX = false;
    public GameObject[] prefabs;
    public Color[] colors;
    

    private List<GameObject> active = new List<GameObject>();

    private float timer = 0;

    private VisualEffect vfx = new VisualEffect();

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
        // make sure to update tag for runtime movement called by other script
        active[active.Count - 1].tag = "env";
        // customize prefab:
        if (randomRotation) active[active.Count - 1].transform.eulerAngles += new Vector3(0, Random.Range(0, 360), 0);
        if (isVFX)
        {
            // save these values as they might be needed for multiple VFX
            float randBias = Random.Range(10, 50);
            float randTimeStep = Random.Range(0.1f, 0.5f);
            float randTimeToBeat = 0.01f;
            float randRestSmoothTime = 0.1f;

            // set values for y velocity effect
            var vfxsync = active[active.Count - 1].GetComponent<VFXSyncYVelocity>();
            vfxsync.bias = randBias;
            vfxsync.timeStep = randTimeStep;
            vfxsync.timeToBeat = randTimeToBeat;
            vfxsync.restSmoothTime = randRestSmoothTime;
            vfxsync.restVelocity = 0;
            vfxsync.beatVelocity = Random.Range(2f, 6f);
            vfxsync.triggerTime = 0.05f;

            // set values for color blend effect
            var vfxsync2 = active[active.Count - 1].GetComponent<VFXSyncColorBlend>();
            vfxsync2.bias = randBias;
            vfxsync2.timeStep = randTimeStep;
            vfxsync2.timeToBeat = randTimeToBeat;
            vfxsync2.restSmoothTime = randRestSmoothTime;
            vfxsync2.blendPercent = Random.Range(1f, 1.5f);

            // set the color of the vfx to a random one in the color list
            vfx = vfxsync.GetComponent<VisualEffect>();
            vfx.SetVector4("color1", colors[Random.Range(0, colors.Length-1)]);

            // set random scale
            float scaleInc = Random.Range(-0.2f, 1.2f);
            active[active.Count - 1].transform.localScale += new Vector3(scaleInc, scaleInc, scaleInc);
            // adjust position based on random scale
            if(scaleInc < 0)
            {
                active[active.Count - 1].transform.position += new Vector3(0,-2f,0);
            }
        } else if (randomScale)
        {
            float scaleInc = Random.Range(-0.2f, 1.5f);
            active[active.Count - 1].transform.localScale += new Vector3(scaleInc, scaleInc, scaleInc);
        }
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
