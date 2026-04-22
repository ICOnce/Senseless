using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadius;

    public LayerMask obstacleMask;
    public LayerMask groundMask;

    Collider2D[] objectsInRange = new Collider2D[1];

    public float meshResolution;

    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    void FindObjectsInRadius()
    {
        Collider2D[] prevArr = objectsInRange;
        objectsInRange = Physics2D.OverlapCircleAll(transform.position, viewRadius, obstacleMask);

        Debug.Log(objectsInRange.Length);

        foreach(Collider2D obj in prevArr)
        {
            if (!objectsInRange.Contains(obj))
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        foreach(Collider2D obj in objectsInRange)
        {
            float dist = Vector2.Distance(transform.position, obj.transform.position);

            Vector2 dir = (obj.transform.position - transform.position).normalized;

            Debug.DrawRay(transform.position, dir, Color.red);

            if (!Physics2D.Raycast(transform.position, dir, dist, groundMask))
            {
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }

    void DrawFieldOfView()
    {
        float stepAngleSize = 360 / meshResolution;

        List<Vector2> viewPoints = new List<Vector2>();
        for( int i = 0; i < meshResolution; i++)
        {
            float angle = stepAngleSize * i;
            if (Physics2D.Raycast(transform.position, DirFromAngle(angle), viewRadius, groundMask))
                viewPoints.Add(Physics2D.Raycast(transform.position, DirFromAngle(angle), viewRadius, groundMask).point);
            else
            {
                viewPoints.Add(transform.position + DirFromAngle(angle) * viewRadius);
            }


                Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle) * viewRadius, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount) * 3];
        vertices[0] = Vector3.zero;
        for( int i = 0;i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        triangles[triangles.Length - 3] = 0;
        triangles[triangles.Length - 2] = vertices.Length - 1;
        triangles[triangles.Length - 1] = 1;

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    Vector3 DirFromAngle(float angleInDregrees)
    {
        return new Vector3(Mathf.Sin(angleInDregrees * Mathf.Deg2Rad), Mathf.Cos(angleInDregrees * Mathf.Deg2Rad),0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawFieldOfView();
        FindObjectsInRadius();
    }
}
