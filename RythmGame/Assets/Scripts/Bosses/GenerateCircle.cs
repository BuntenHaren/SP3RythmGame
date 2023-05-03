using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class GenerateCircle : MonoBehaviour
{
    private MeshFilter mf;
    private MeshCollider collider;
    
    private void Start()
    {
        mf = GetComponent<MeshFilter>();
        collider = GetComponent<MeshCollider>();
    }

    public Mesh CreateCircleMesh(int sides, float radius, float sectorAngle)
    {
        Vector3[] vertices = GetPointsOnRadius(radius, sides, sectorAngle);

        int[] triangles = DrawFilledTriangles(sides);

        Vector3[] normals = GetNormals(vertices);
        
        Vector2[] uvs = GetUVs(vertices, radius);

        //Generate the mesh based on everything done
        Mesh createdMesh = new Mesh();
        
        createdMesh.vertices = vertices;
        createdMesh.triangles = triangles;
        createdMesh.normals = normals;
        createdMesh.uv = uvs;
        
        return createdMesh;
    }

    public Mesh CreateHollowCircle(int sides, float innerRadius, float outerRadius, float angleInDegrees)
    {
        //Vertices
        List<Vector3> pointsList = new List<Vector3>();
        Vector3[] outerPoints = GetPointsOnRadius(outerRadius, sides, angleInDegrees);
        Vector3[] innerPoints = GetPointsOnRadius(innerRadius, sides, angleInDegrees);
        pointsList.AddRange(outerPoints);
        pointsList.AddRange(innerPoints);
        Vector3[] vertices = pointsList.ToArray();

        int[] triangles = DrawHollowTriangles(vertices);

        Vector3[] normals = GetNormals(vertices);

        Vector2[] uvs = GetUVs(vertices, outerRadius);
        
        Mesh createdMesh = new Mesh();
        
        createdMesh.vertices = vertices;
        createdMesh.triangles = triangles;
        createdMesh.normals = normals;
        createdMesh.uv = uvs;
        
        return createdMesh;
    }

    private Vector3[] GetPointsOnRadius(float radius, int sides, float angleInDegrees)
    {
        List<Vector3> pointsList = new List<Vector3>();
        float x;
        float y;
        pointsList.Add(Vector3.zero);
        for (int i = 0; i < sides + 2; i ++)
        {
            x = radius * Mathf.Sin((angleInDegrees * Mathf.Deg2Rad * i) / sides);
            y = radius * Mathf.Cos((angleInDegrees * Mathf.Deg2Rad * i) / sides);
            pointsList.Add(new Vector3(x, 0, y));
        }
        Vector3[] points = pointsList.ToArray();

        return points;
    }

    private int[] DrawHollowTriangles(Vector3[] points)
    {
        int sidesAmount = points.Length / 2;
        List<int> newTriangles = new List<int>();
        
        for(int i = 0; i < sidesAmount; i++)
        {
            int outerIndex = i;
            int innerIndex = i + sidesAmount;
            
            //First triangle
            newTriangles.Add(outerIndex);
            newTriangles.Add(innerIndex);
            newTriangles.Add((i + 1) % sidesAmount);
            
            //Second triangle
            newTriangles.Add(outerIndex);
            newTriangles.Add(sidesAmount + (sidesAmount + i - 1) % sidesAmount);
            newTriangles.Add(outerIndex + sidesAmount);
        }

        int[] triangles = newTriangles.ToArray();
        
        return triangles;
    }

    private int[] DrawFilledTriangles(int numberOfSides)
    {
        List<int> trianglesList = new List<int>();
        
        for(int i = 0; i < numberOfSides; i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i+1);
            trianglesList.Add(i+2);
        }
        int[] triangles = trianglesList.ToArray();
        
        return triangles;
    }

    private Vector3[] GetNormals(Vector3[] vertices)
    {
        List<Vector3> normalsList = new List<Vector3>();
        
        for (int i = 0; i < vertices.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        
        Vector3[] normals = normalsList.ToArray();
        return normals;
    }

    private Vector2[] GetUVs(Vector3[] vertices, float radius)
    {
        Vector2[] uvs = new Vector2[vertices.Length];
        
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / (radius*2) + 0.5f, vertices[i].y / (radius*2) + 0.5f);
        }

        return uvs;
    }

    public void SetMesh(Mesh targetMesh)
    {
        mf.mesh.Clear();
        mf.mesh = targetMesh;
    }
}
