using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlmostFarmland : MonoBehaviour
{
    public FarmManager farmManager;

    public List<GameObject> farmlands;
    public GameObject farmland;
    public float shotDis;

    void Update()
    {
        FindAlmostFarmland();
    }

    void FindAlmostFarmland()
    {
        if (farmManager.isGrab)
        {
            farmlands = new List<GameObject>(GameObject.FindGameObjectsWithTag("Farmland"));
            shotDis = Vector3.Distance(transform.position, farmlands[0].transform.position);

            farmland = farmlands[0];

            foreach(GameObject grid in farmlands)
            {
                grid.GetComponent<BoxCollider>().enabled = false;
                float dis = Vector3.Distance(transform.position, grid.transform.position);

                if(dis < shotDis)
                {
                    farmland = grid;
                    shotDis = dis;
                }
            }
            farmland.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
