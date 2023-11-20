using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : SingletonMono<Grid>
{

    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadious;
    [SerializeField] private GameObject prefab;

    float thikness;
    private List<GameObject> farmLands;
    int gridXSize, gridYSize;
    float nodeDiameter;
    Node[,] grid;

    void Start()
    {
        nodeDiameter = prefab.GetComponent<BoxCollider>().bounds.size.x; // ������Ʈ�� ���Ե� �� ���� ũ�� �Ҵ�
        gridXSize = Mathf.RoundToInt(gridWorldSize.x * nodeDiameter);
        gridYSize = Mathf.RoundToInt(gridWorldSize.y * nodeDiameter);
        thikness = prefab.GetComponent<BoxCollider>().bounds.size.y;
        farmLands = new List<GameObject>();
        GenerateGrid();
    }

    private void Update()
    {
        //GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid = new Node[gridXSize, gridYSize];
        Vector3 topLeftNodePosition = transform.position - (Vector3.right * gridWorldSize.x / 2f) - (Vector3.forward * gridWorldSize.y / 2f);

        for (int x = 0; x < gridXSize; x++)
        {
            for (int y = 0; y < gridYSize; y++)
            {
                Vector3 worldPoint = topLeftNodePosition + 
                    Vector3.right * (x * nodeDiameter + nodeRadious) + 
                    Vector3.forward * (y * nodeDiameter * 0.95f + nodeRadious);
                //bool walkable = !Physics.CheckSphere(worldPoint, nodeRadious, unwalkableMask);

                grid[x, y] = new Node(worldPoint);
            }
        }

        foreach (Node node in grid)
        {
            farmLands.Add(Instantiate(prefab, node.worldPosition, Quaternion.identity, transform));
        }
        
    }
    public void ObjActive()
    {
        foreach(GameObject soils in farmLands)
        {
            GameObject afterSoil = soils.transform.GetChild(1).gameObject; //������ ���� ����
            afterSoil.SetActive(!afterSoil.activeSelf);
            if(soils.transform.childCount > 2) // ������Ʈ 'SoilGenerate'�� �ڽ� ������Ʈ�� ������ �Ǻ�. ������ ������ ��.
            {
                afterSoil = soils.transform.GetChild(2).transform.GetChild(0).gameObject;  //������ ����.
                afterSoil.SetActive(!afterSoil.activeSelf);
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, thikness, gridWorldSize.y));
        /*
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter-.1f));
            }
        }*/
    }
    
}
