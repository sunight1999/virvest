﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : SingletonMono<Grid>
{

    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadious;
    [SerializeField] private GameObject prefab;

    float thikness;
    private HashSet<GameObject> farmLands;
    int gridXSize, gridYSize;
    float nodeDiameter;
    Node[,] grid;
    //public static Grid Instance { get; private set; }
   
    void Start()
    {
        nodeDiameter = prefab.GetComponent<BoxCollider>().bounds.size.x; // 오브젝트에 포함된 한 노드당 크기 할당
        gridXSize = Mathf.RoundToInt(gridWorldSize.x * nodeDiameter);
        gridYSize = Mathf.RoundToInt(gridWorldSize.y * nodeDiameter);
        thikness = prefab.GetComponent<BoxCollider>().bounds.size.y;
        farmLands = new HashSet<GameObject>();
        GenerateGrid();
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

                grid[x, y] = new Node(worldPoint);
            }
        }

        foreach (Node node in grid)
        {
            farmLands.Add(Instantiate(prefab, node.worldPosition, Quaternion.identity, transform));
        }

    }

    /*
    public void ObjActive()
    {
        foreach (GameObject soils in farmLands)
        {
            GameObject afterSoil = soils.transform.GetChild(1).gameObject; //모종의 땅을 숨김
            afterSoil.SetActive(!afterSoil.activeSelf);
            if (soils.transform.childCount > 2) // 오브젝트 'SoilGenerate'의 자식 오브젝트의 개수로 판별. 각별히 유의할 것.
            {
                GameObject easd = soils.transform.GetChild(2).transform.GetChild(0).gameObject;  //모종을 숨김.
                easd.SetActive(!afterSoil.activeSelf);
            }
        }
    }
    */

    public void UpdateSeeding()
    {
        foreach(GameObject soil in farmLands)
        {
            if(soil.transform.childCount > 2)
            {
                SeedingGrowth sg = soil.transform.GetChild(2).GetComponent<SeedingGrowth>();
                sg.UpdateSeeding();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, thikness, gridWorldSize.y));
    }
}