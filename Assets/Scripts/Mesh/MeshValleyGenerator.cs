using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Creates a rectangular mesh with vertex count provided by inspector values
[RequireComponent(typeof(MeshFilter))]
public class MeshValleyGenerator : MonoBehaviour
{
    // init
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // INSPECTOR VALUES
    public int xSize = 20;
    public int zSize = 20;
    public float peakHeight = 4f;

    // Awake is called before Start()
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    /// <summary>
    /// Create vertices and attach triangles properly to create the mesh
    /// </summary>
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.Cos(6.28f * x / xSize) * peakHeight * Mathf.PerlinNoise(x * .3f, z * .3f);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vertex = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris] = vertex;
                triangles[tris + 1] = vertex + xSize + 1;
                triangles[tris + 2] = vertex + 1;
                triangles[tris + 3] = vertex + 1;
                triangles[tris + 4] = vertex + xSize + 1;
                triangles[tris + 5] = vertex + xSize + 2;


                vertex++;
                tris += 6;

            }
            vertex++;
        }
    }

    /// <summary>
    /// Always update the mesh in case vertices change
    /// </summary>
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
