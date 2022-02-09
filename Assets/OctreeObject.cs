using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Octree;

public class OctreeObject : MonoBehaviour
{
    public Octree.Tree<int> tree;

    public Mesh mesh;

    public bool ProjectToSphere = false;
    public Vector3 Dimensions = new Vector3(1f, 1f, 1f);
    public Vector3 Cells = new Vector3(10, 10, 10);
    public float Depth = 1f;

    private List<int> faces = new List<int>();

    public void Render()
    {
        MeshFilter filter = GetComponent<MeshFilter>();

        if (mesh == null) mesh = new Mesh();
        filter.mesh = mesh;
    }

    public void CreateMesh()
    {
        if (mesh == null) mesh = new Mesh();
        else mesh.Clear();

        List<Vector3> vertices = new List<Vector3>();
        faces.Clear();

        int A = (int) Cells.x;
        int B = (int) Cells.z;
        int C = (int) Cells.y;

        float dA = 2f / A;
        float dB = 2f / B;
        float dC = C == 1 ? 0f : -Depth / (C - 1);

        float a0 = -1f + dA / 2f;
        float b0 = -1f + dB / 2f;
        float c0 = 1f;

        for (int f = 0; f < 6; f++)
        {
            int d = f / 2;
            
            for (int a = 0; a < A; a++)
            {
                for (int b = 0; b < B; b++)
                {
                    for (int c = 0; c < C; c++)
                    {
                        float sign = f % 2 == 0 ? 1 : -1;

                        float xa = a * dA + a0;
                        float xb = b * dB + b0;

                        float xc = c * dC + c0;

                        Vector3 v = new Vector3();
                        // new Vector3(xc * sign, xb * xc, xa * xc);
                        if (d == 0) // X
                        {
                            v.x = xc * sign;
                            v.y = xb * xc;
                            v.z = xa * xc;
                        } else if (d == 1) // Y
                        {
                            v.x = xa * xc;
                            v.y = xc * sign;
                            v.z = xb * xc;
                        } else if (d == 2) // Z
                        {
                            v.x = xb * xc;
                            v.y = xa * xc;
                            v.z = xc * sign;
                        }

                        Vector3 vo = (v / v.magnitude) * xc;

                        vertices.Add(ProjectToSphere ? vo : v);
                        faces.Add(f);
                    }
                }
            }
        }

        mesh.SetVertices(vertices);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!ProjectToSphere)
        {
            Gizmos.DrawWireCube(this.transform.position, this.transform.TransformVector(Vector3.one * 2f));
            Gizmos.DrawWireCube(this.transform.position, this.transform.TransformVector(Vector3.one * 2f * (1f - Depth)));
        }
        else
        {
            Gizmos.DrawWireSphere(this.transform.position, transform.localScale.x);
            Gizmos.DrawWireSphere(this.transform.position, transform.localScale.x * (1f - Depth));
        }

        if (mesh != null)
        {
            for (int i = 0; i < faces.Count; i++)
            {
                Gizmos.color = Color.red;
                if (faces[i] == 1) Gizmos.color = Color.magenta;
                else if (faces[i] == 2) Gizmos.color = Color.blue;
                else if (faces[i] == 3) Gizmos.color = Color.cyan;
                else if (faces[i] == 4) Gizmos.color = Color.green;
                else if (faces[i] == 5) Gizmos.color = Color.yellow;

                Vector3 vertex = mesh.vertices[i];
                Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.5f);
            }
        }
    }
}
