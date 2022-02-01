using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Octree;

public class OctreeObject : MonoBehaviour
{
    public Octree.Tree<int> tree;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(10f, 10f, 10f));
    }
}
