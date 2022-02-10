using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Icosphere : ScriptableObject
{
    public float Radius = 6371f; // in m
    
    public float Depth = 0.1f;
    // (radial distance, sphere surface) or (rho, s) 
    public Vector2 Resolution = new Vector2(100f, 100f);  // in km

    [HideInInspector]
    public Vector3[] vertices;
    [HideInInspector]
    public int[] faces;

    public void Generate()
    {
        
    }
}
