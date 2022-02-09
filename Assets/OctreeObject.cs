using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Octree;

public class OctreeObject : MonoBehaviour
{
    Mesh mesh;

    public bool ProjectToSphere = false;

    public Sphere sphere;

    public void Render()
    {
        MeshFilter filter = GetComponent<MeshFilter>();

        if (mesh == null) mesh = new Mesh();
        filter.mesh = mesh;

        foreach (FaceObject faceObject in GetComponentsInChildren<FaceObject>()) faceObject.Render();
    }

    public void CreateMesh()
    {
        if (mesh == null) mesh = new Mesh();
        else mesh.Clear();

        if (sphere != null)
        {
            sphere.Generate();
            mesh.SetVertices(ProjectToSphere ? sphere.vertices : sphere.cubedVertices);

            foreach (FaceObject faceObject in GetComponentsInChildren<FaceObject>())
            {
                faceObject.sphere = sphere;
                faceObject.ProjectToSphere = ProjectToSphere;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        float Depth = sphere == null ? 1f : sphere.Depth;
        
        Gizmos.color = Color.red;
        if (!ProjectToSphere)
        {
            Gizmos.DrawWireCube(this.transform.position, this.transform.TransformVector(Vector3.one * 2f));
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(this.transform.position, this.transform.TransformVector(Vector3.one * 2f * (1f - Depth)));
        }
        else
        {
            Gizmos.DrawWireSphere(this.transform.position, transform.localScale.x);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(this.transform.position, transform.localScale.x * (1f - Depth));
        }
    }
}
