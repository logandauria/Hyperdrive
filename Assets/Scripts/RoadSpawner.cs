using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{

    public Vector3 initPos;
    public float speed;

    public GameObject roads;
    private float offset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initPos = roads.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        roads.transform.position = new Vector3(roads.transform.position.x,
        roads.transform.position.y,roads.transform.position.z - speed);

        if(roads.transform.position.z < 0)
        {
            roads.transform.position = initPos;
        }
    }

    // Update is called once per frame
    public void MoveRoad()
    {
        
    }
}
