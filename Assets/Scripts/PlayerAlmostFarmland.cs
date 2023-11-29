using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class PlayerAlmostFarmland : MonoBehaviour
{
    public XRDirectInteractor interactor;

    public FarmManager farmManager;

    public List<GameObject> farmlands;
    public GameObject farmland;
    public float shotDis;

    void Start()
    {
        //farmManager = GameObject.FindObjectOfType<FarmManager>();
    }

    void Update()
    {
        FindAlmostFarmland();
    }

    int GetHandObjLayer()
    {
        // 현재 손에 잡고 있는 오브젝트를 가져옵니다.
        XRBaseInteractable interactable = interactor.selectTarget;
        if (interactable.gameObject.tag == "Compost")
            return 9;
        else if (interactable.gameObject.tag == "FarmEquipment P")
            return 10;
        else if(interactable.gameObject.tag == "FarmEquipment S")
            return 11;
        else
            return 9;
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
                grid.GetComponents<BoxCollider>()[0].enabled = false;
                grid.GetComponents<BoxCollider>()[1].enabled = false;
                float dis = Vector3.Distance(transform.position, grid.transform.position);

                if(dis < shotDis && grid.layer == GetHandObjLayer())
                {
                    farmland = grid;
                    shotDis = dis;
                }
            }
            farmland.GetComponents<BoxCollider>()[0].enabled = true;
            farmland.GetComponents<BoxCollider>()[1].enabled = true;
            farmManager.farmland = farmland.GetComponent<Ground>();
        }
    }
}
