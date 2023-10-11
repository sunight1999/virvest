using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadious;
    public GameObject prefab;

    int gridXSize, gridYSize;
    float nodeDiameter;
    Node[,] grid;

    void Start()
    {
        nodeDiameter = prefab.GetComponent<BoxCollider>().bounds.size.x; // 오브젝트에 포함된 한 노드당 크기 할당
        gridXSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridYSize = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        GenerateGrid();
    }

    private void Update()
    {
        //GenerateGrid();
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

        foreach (Node node in grid) 
            Instantiate(prefab, node.worldPosition, Quaternion.identity, transform).GetComponent<Ground>(); // 오브젝트 생성 및 땅 속성 추가
        
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, prefab.GetComponent<BoxCollider>().bounds.size.y, gridWorldSize.y));

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
