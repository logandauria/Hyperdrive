using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeshSync : AudioSyncer
{
    int count = 100;
    float range = 0;
    private float centerNum;
    // calculation of dist from centerpoint in mesh for each vertice
    private float[] ranges;

    public int force;
    public GameObject player;

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

    public override void OnBeat()
    {
        base.OnBeat();
        createVelocities();

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
            if(ranges[i] < 7)
            {
                displacedVertices[i].y = displacedVertices[i].y/1.1f;
            }
            // random sample of half
            else if (Random.Range(0, 3) == 1)
            {
                // apply velocity
                displacedVertices[i] += velocity * Time.deltaTime;
            }


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
