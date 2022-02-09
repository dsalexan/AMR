using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FaceObject : MonoBehaviour
{
    Mesh mesh;
    
    public Sphere.Face Face;

    public bool ProjectToSphere = false;
    public Sphere sphere;

    public void Render()
    {
        MeshFilter filter = GetComponent<MeshFilter>();

        if (mesh == null) mesh = new Mesh();
        filter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (sphere != null)
        {
            for (int i = 0; i < sphere.faces.Length; i++)
            {
                int f = sphere.faces[i];
                if (f != (int)Face) continue;
                
                Vector3 vertex = ProjectToSphere ? sphere.vertices[i] : sphere.cubedVertices[i];

                Gizmos.color = Color.red;
                if (sphere.faces[i] == 1) Gizmos.color = Color.magenta;
                else if (sphere.faces[i] == 2) Gizmos.color = Color.green;
                else if (sphere.faces[i] == 3) Gizmos.color = Color.yellow;
                else if (sphere.faces[i] == 4) Gizmos.color = Color.blue;
                else if (sphere.faces[i] == 5) Gizmos.color = Color.cyan;

                Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.5f);
            }
        }
    }
}
