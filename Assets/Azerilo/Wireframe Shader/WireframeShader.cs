using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeShader : MonoBehaviour
{
	public Material wireframeMaterial;
	private GameObject wireframeObject;
	private Mesh bakedMesh;
	private MeshFilter meshFilter;
	bool hasMesh = false;
	bool isSkinned = false;

	void Start()
	{
        if (wireframeMaterial == null)
        {
			Debug.LogError("The Wireframe Material field is empty. You must assign the wireframe material!");

		}

        if ((GetComponent<MeshFilter>() != null) || (GetComponent<SkinnedMeshRenderer>() != null))        
			hasMesh = true;

        if (hasMesh)
        {
			bakedMesh = new Mesh();
			wireframeObject = new GameObject("Wireframe");
			wireframeObject.transform.SetParent(transform);
			wireframeObject.transform.localPosition = Vector3.zero;
			wireframeObject.transform.localScale = new Vector3(1, 1, 1);
			wireframeObject.transform.localRotation = Quaternion.identity;

			meshFilter = GetComponent<MeshFilter>();

			if (meshFilter == null)
				isSkinned = true;

			if (isSkinned)
			{
				var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
				bakedMesh = BakeMesh(skinnedMeshRenderer.sharedMesh);
				var wireframeRenderer = wireframeObject.AddComponent<SkinnedMeshRenderer>();
				wireframeRenderer.bones = skinnedMeshRenderer.bones;
				wireframeRenderer.sharedMesh = bakedMesh;
				wireframeRenderer.material = wireframeMaterial;
			}
			else
			{				
				bakedMesh = BakeMesh(meshFilter.sharedMesh);
				wireframeObject.AddComponent<MeshRenderer>();
				wireframeObject.AddComponent<MeshFilter>();
				wireframeObject.GetComponent<MeshFilter>().sharedMesh = bakedMesh;
				wireframeObject.GetComponent<MeshRenderer>().material = wireframeMaterial;
			}
		}
		else
        {
			Debug.LogError(name + " does not have a mesh!");
        }
	}

	void Update()
    {
		bakedMesh = BakeMesh(meshFilter.sharedMesh);
    }

	private Mesh BakeMesh(Mesh originalMesh)
	{
		var maxVerts = 2147483647;
		var meshNor = originalMesh.normals;
		var meshTris = originalMesh.triangles;
		var meshVerts = originalMesh.vertices;		
		var boneW = originalMesh.boneWeights;		
		var vertsNeeded = meshTris.Length;

		if (vertsNeeded > maxVerts)
		{	
			Debug.LogError("The mesh has so many vertices that Unity could not create it!");
			return null;
		}

		var resultMesh = new Mesh();
		resultMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;		
		var resultVerts = new Vector3[vertsNeeded];
		var resultUVs = new Vector2[vertsNeeded];
		var resultTris = new int[meshTris.Length];
		var resultNor = new Vector3[vertsNeeded];
		var boneWLen = (boneW.Length > 0) ? vertsNeeded : 0;
		var resultBW = new BoneWeight[boneWLen]; 
		
		for (var i = 0; i < meshTris.Length; i+=3)
		{
			resultVerts[i] = meshVerts[meshTris[i]];
			resultVerts[i+1] = meshVerts[meshTris[i+1]];
			resultVerts[i+2] = meshVerts[meshTris[i+2]];		
			resultUVs[i] = new Vector2(0f,0f);
			resultUVs[i+1] = new Vector2(1f,0f);
			resultUVs[i+2] = new Vector2(0f,1f);
			resultTris[i] = i;
			resultTris[i+1] = i+1;
			resultTris[i+2] = i+2;
			resultNor[i] = meshNor[meshTris[i]];
			resultNor[i+1] = meshNor[meshTris[i+1]];
			resultNor[i+2] = meshNor[meshTris[i+2]];

			if (resultBW.Length > 0)
			{
				resultBW[i] = boneW[meshTris[i]];
				resultBW[i+1] = boneW[meshTris[i+1]];
				resultBW[i+2] = boneW[meshTris[i+2]];
			}
		}

		resultMesh.vertices = resultVerts;
		resultMesh.uv = resultUVs;
		resultMesh.triangles = resultTris;
		resultMesh.normals = resultNor;
		resultMesh.bindposes = originalMesh.bindposes;
		resultMesh.boneWeights = resultBW;

		return resultMesh;
	}

	

}
