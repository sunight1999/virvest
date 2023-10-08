using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadious;

    int gridXSize, gridYSize;
    float nodeDiameter;
    Node[,] grid;

    void Start()
    {
        nodeDiameter = nodeRadious * 2;
        gridXSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridYSize = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Node[gridXSize, gridYSize];
        Vector3 topLeftNodePosition = transform.position - (Vector3.right * gridWorldSize.x / 2f) - (Vector3.forward * gridWorldSize.y / 2f);

        for (int x = 0; x < gridXSize; x++)
        {
            for (int y = 0; y < gridYSize; y++)
            {
                Vector3 worldPoint = topLeftNodePosition + 
                    Vector3.right * (x * nodeDiameter + nodeRadious) + 
                    Vector3.forward * (y * nodeDiameter + nodeRadious);
                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadious, unwalkableMask);

                grid[x, y] = new Node(worldPoint, walkable);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter-.1f));
            }
        }
    }
}
