using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 worldPosition;
    public bool walkable;

    public Node(Vector3 position)
    {
        this.worldPosition = position;
    }
}