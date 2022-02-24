using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CityMeshGen : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    public int ySize = 60;

    // Awake is called before Start()
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                for (int y = 0; y <= ySize; y++)
                {
                    // only create vertices every 5 step for x and z, 10 for y
                    if (y % 10 == 0 && z % 5 == 0 && x % 5 == 0)
                    {
                        vertices[i] = new Vector3(x, y, z);
                        i++;
                    }
                }
            }
        }

        triangles = new int[vertices.Length * 6];

        int vertex = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++) {
                    triangles[tris] = vertex;
                    triangles[tris + 1] = vertex + xSize + 1;
                    triangles[tris + 2] = vertex + 1;
                    triangles[tris + 3] = vertex + 1;
                    triangles[tris + 4] = vertex + xSize + 1;
                    triangles[tris + 5] = vertex + xSize + 2;


                    vertex++;
                    tris += 6;
                }
            }
            vertex++;
        }


    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
    }

}
