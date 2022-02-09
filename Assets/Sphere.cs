using System;
using UnityEngine;

[CreateAssetMenu]
public class Sphere : ScriptableObject
{
    public enum Face
    {
        PX, // 0
        NX,
        PY,
        NY,
        PZ,
        NZ
    }
    
    public float Radius = 6371f; // in m
    
    public float Depth = 0.1f;
    // (radial distance, sphere surface) or (rho, s) 
    public Vector2 Resolution = new Vector2(100f, 100f);  // in km

    [HideInInspector]
    public Vector3[] vertices;
    [HideInInspector]
    public int[] faces;
    
    [HideInInspector]
    public Vector3[] cubedVertices;

    public void Generate()
    {
        int F = 6;

        float RadiusInM = Radius * 1e6f;

        float depthResolution = Resolution.x * 1e6f; // in m
        float surfaceResolution = Resolution.y * 1e6f; // in m

        float areaOfPlanet = 4f * Mathf.PI * (RadiusInM * RadiusInM); // in m
        float areaOfRegion = 4f * Mathf.PI * (surfaceResolution * surfaceResolution);

        int Ρ = Math.Max(1, Mathf.CeilToInt((Depth * RadiusInM) / depthResolution)); // number of cells in radial/depth axis
        float S2 = areaOfPlanet / (areaOfRegion * F);
        int S = Mathf.CeilToInt(Mathf.Sqrt(S2));

        int N = S * S * F * Ρ;

        vertices = new Vector3[N];
        faces = new int[N];
        
        cubedVertices = new Vector3[N];
        
        // calculate deltas and initial positions
        float dS = 2f / S;
        float dρ = Ρ == 1 ? 0f : -Depth / (Ρ - 1);

        float s0 = -1f + dS / 2f;
        float ρ0 = 1f;
        
        // generate faces and vertices
        int i = 0;
        for (int f = 0; f < 6; f++)
        {
            int d = f / 2;
            
            for (int a = 0; a < S; a++)
            {
                for (int b = 0; b < S; b++)
                {
                    for (int ρ = 0; ρ < Ρ; ρ++)
                    {
                        float sign = f % 2 == 0 ? 1 : -1;

                        float xa = a * dS + s0;
                        float xb = b * dS + s0;

                        float xρ = ρ * dρ + ρ0;

                        Vector3 v = new Vector3();
                        switch (d)
                        {
                            case 0: // X
                                v.x = xρ * sign;
                                v.y = xb * xρ;
                                v.z = xa * xρ;
                                break;
                            case 1: // Y
                                v.x = xa * xρ;
                                v.y = xρ * sign;
                                v.z = xb * xρ;
                                break;
                            case 2: // Z
                                v.x = xb * xρ;
                                v.y = xa * xρ;
                                v.z = xρ * sign;
                                break;
                        }

                        Vector3 vo = (v / v.magnitude) * xρ;

                        cubedVertices[i] = v;
                        vertices[i] = vo;
                        faces[i] = f;
                        i++;
                    }
                }
            }
        }
    }
}

