using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public BoxCollider pitchForkCol; //�簥���� ���� �ݶ��̴�
    public int plowCount; //�簥���� �� ���� ī����

    public GameObject soil; // ������
    public Material[] gridSoils; //�� �־��� �� ���� ��Ʈ����
    int gridSoilsIndex = 0;

    public bool isGrab = true;

    void Start()
    {
        isGrab = false;
    }

    void Update()
    {
        
    }

    public void OnGrab()
    {
        isGrab = true;
    }

    public void OffGrab()
    {
        isGrab = false;
    }

    public void WateringToSoil(GameObject gridSoil)
    {
        float moisture = gridSoil.GetComponentInParent<Ground>().moisture;
        MeshRenderer gridSoilMat = gridSoil.GetComponent<MeshRenderer>();

        gridSoilMat.materials[1] = gridSoils[0];

        //if (gridSoilMat.materials.Length > 1 && gridSoilMat.materials[1].color.g > 200)
        //{
        //    moisture += Time.deltaTime;
        //    gridSoilMat.materials[1].color = new Color(255 - moisture, 255 - moisture, 255 - moisture, 255);
        //}
        //else if (gridSoilMat.materials.Length < 2)
        //{
        //    gridSoilMat.materials[1] = gridSoils[gridSoilsIndex];
        //    gridSoilsIndex++;
        //}
        //else return;
    }
}
