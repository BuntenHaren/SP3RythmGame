using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCircle : MonoBehaviour
{
    [SerializeField]
    private int sides;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float sectorAngle;
    
    private MeshFilter mf;
    private MeshCollider collider;
    
    private void Start()
    {
        mf = GetComponent<MeshFilter>();
        collider = GetComponent<MeshCollider>();
    }

    private Mesh CreateCircleMesh()
    {
        //verticies
        List<Vector3> verticesList = new List<Vector3>();
        float x;
        float y;
        for (int i = 0; i < sides; i ++)
        {
            x = radius * Mathf.Sin((sectorAngle * Mathf.Deg2Rad * i) / sides);
            y = radius * Mathf.Cos((sectorAngle * Mathf.Deg2Rad * i) / sides);
            verticesList.Add(new Vector3(x, y, 0f));
        }
        Vector3[] vertices = verticesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int>();
        for(int i = 0; i < (sides-2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i+1);
            trianglesList.Add(i+2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3>();
        for (int i = 0; i < vertices.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();
        
        //Fix the UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / (radius*2) + 0.5f, vertices[i].y / (radius*2) + 0.5f);
        }

        //Generate the mesh based on everything done
        Mesh createdMesh = new Mesh();
        
        createdMesh.vertices = vertices;
        createdMesh.triangles = triangles;
        createdMesh.normals = normals;
        createdMesh.uv = uvs;
        
        return createdMesh;
    }

    private void SetMesh(Mesh targetMesh)
    {
        mf.mesh = targetMesh;
    }

    private void Update()
    {
        SetMesh(CreateCircleMesh());
    }
}
