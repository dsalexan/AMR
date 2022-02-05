using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OctreeObject))]
public class OctreeEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        OctreeObject obj = (OctreeObject)target;
        
        if (GUILayout.Button("Create")) obj.CreateMesh();
        if (GUILayout.Button("Render")) obj.Render();
    }
}
