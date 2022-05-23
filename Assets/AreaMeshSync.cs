using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeshSync : AudioSyncer
{
    int count = 100;
    float range = 0;
    private float centerNum;
    // calculation of dist from centerpoint in mesh for each vertice
    private float[] ranges;

    public int force;
    public GameObject player;
    public int xSize = 0;
    public int ySize = 0;
    public float flatRadius = 5f;


    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices, vertexVelocities;



    // creates pseudo-random velocities of each vertex
    void createVelocities()
    {
        for (int i = 0; i < vertexVelocities.Length; i++)
        {
            float nf = displacedVertices[i].y;
            if (nf > -1 && nf < 0)
            {
                nf = -1;
            }
            else if (nf < 1 && nf > 0)
            {
                nf = 1;
            }
            vertexVelocities[i] = Vector3.up * Random.Range(0, force) * -nf;
        }
    }

    // Calculates velocities for a random non edge point and it's surrounding points. Surrounding points will be given less velocity.
    void createAreaVelocity()
    {
        // Radius of area to be influenced
        // Todo: Randomize?
        int areaRadius = Random.Range(2, Mathf.Min(xSize, ySize) / 2 - 5);
        int randVel = Random.Range(0, force);
        // pick a random non-edge point 
        int randX = Random.Range(areaRadius + 1, xSize - (areaRadius + 1));
        int randY = Random.Range(areaRadius + 1, ySize - (areaRadius + 1));
        // midpoint's index in the vertex arrays
        int midIndex = ySize * randX + randY;

        // normalizing force
        float nf = -displacedVertices[midIndex].y;

        // calculate base velocity now to prevent repeated math
        Vector3 preVelocity = Vector3.up * randVel;


        // Apply full velocity to midpoint
        vertexVelocities[midIndex] = preVelocity;

        // Create smaller velocities of the area around the midpoint
        for (int x = -areaRadius; x <= areaRadius; x++)
        {
            for (int y = -areaRadius; y <= areaRadius; y++)
            {
                if (x != 0 && y != 0)
                {
                    // current index in the vertex arrays
                    int curIndex = midIndex + y + (x * ySize);
                    // recalculate normal force for selected vertex
                    nf = -displacedVertices[curIndex].y;
                    if (nf > -1 && nf < 0)
                    {
                        nf = -1;
                    }
                    else if (nf < 1 && nf > 0)
                    {
                        nf = 1;
                    }
                    vertexVelocities[curIndex] = preVelocity * nf / Mathf.Sqrt((Mathf.Abs(x) + Mathf.Abs(y)));
                }
            }
        }
    }

    public override void OnBeat()
    {
        base.OnBeat();
        //createVelocities();
        int areas = Random.Range(5, 8);
        for (int x = 0; x < 1; x++)
        {
            createAreaVelocity();
        }
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();

        calcRanges();

        for (int i = 0; i < vertexVelocities.Length; i++)
        {
            Vector3 velocity = vertexVelocities[i];
            vertexVelocities[i].y *= .99f;


            // if range is over constant, vertex height will div towards 0
            if (ranges[i] < flatRadius)
            {
                displacedVertices[i].y = displacedVertices[i].y / 1.1f;
            }
            // random sample of half
            //else if (Random.Range(0, 3) == 1)
            //{
                // apply velocity
                displacedVertices[i] += velocity * Time.deltaTime;
            //}


            //Debug.Log(displacedVertices[i].x + " " + displacedVertices[i].z + " | range: " + range);
        }

        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = deformingMesh;

    }


    void calcRanges()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // calculates vertex range from car
            Vector3 rangeVector = (displacedVertices[i] + this.transform.position - player.transform.position);
            rangeVector.z -= 5;
            ranges[i] = rangeVector.magnitude;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        centerNum = Mathf.Sqrt(displacedVertices.Length) / 2;
        ranges = new float[displacedVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
        vertexVelocities = new Vector3[displacedVertices.Length];

        createVelocities();

    }
}
