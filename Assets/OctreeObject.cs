using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Octree;

public class OctreeObject : MonoBehaviour
{
    public Octree.Tree<int> tree;

    public Mesh mesh;

    public Vector3 Dimensions = new Vector3(1f, 1f, 1f);
    public Vector3 Cells = new Vector3(10, 10, 10);

    public void Render()
    {
        MeshFilter filter = GetComponent<MeshFilter>();

        if (mesh == null) mesh = new Mesh();
        filter.mesh = mesh;
    }

    public void CreateMesh()
    {
        if (mesh == null) mesh = new Mesh();

        int I = (int)Cells[0];
        int J = (int)Cells[1];
        int K = (int)Cells[2];

        float X = Dimensions[0];
        float Y = Dimensions[1];
        float Z = Dimensions[2];

        float dX = X / (I - 1);
        float dY = Y / (J - 1);
        float dZ = Z / (K - 1);

        List<Vector3> vertices = new List<Vector3>();

        float x0 = -X / 2f;
        float y0 = -Y / 2f;
        float z0 = -Z / 2f;
        
        for (int i = 0; i < I; i++)
        {
            for (int j = 0; j < J; j++)
            {
                for (int k = 0; k < K; k++)
                {
                    Vector3 vertex = new Vector3
                    {
                        x = i * dX + x0,
                        y = j * dY + y0,
                        z = k * dZ + z0
                    };
                    vertices.Add(vertex);
                }
            }
        }

        mesh.SetVertices(vertices);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, this.transform.TransformVector(Dimensions));

        if (mesh != null)
        {
            Gizmos.color = Color.red;
            foreach (Vector3 vertex in mesh.vertices)
            {
                Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.1f);
            }
        }
    }
}
