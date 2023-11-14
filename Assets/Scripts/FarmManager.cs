using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public BoxCollider pitchForkCol; //밭갈개의 판정 콜라이더
    public int plowCount; //밭갈개의 땅 갈기 카운터

    public GameObject soil; // 경작지
    public Material[] gridSoils; //물 주었을 떄 넣을 메트리얼
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
        Renderer gridSoilMat = gridSoil.GetComponent<Renderer>();

        if (gridSoilMat.materials[0].name != "Soil" && gridSoilMat.materials[0].color.g > 0.7)
        {
            gridSoilMat.materials[0].color = 
                new Color(gridSoilMat.materials[0].color.r - Time.deltaTime,
                gridSoilMat.materials[0].color.g - Time.deltaTime,
                gridSoilMat.materials[0].color.b - Time.deltaTime, 1);
        }
        else if (gridSoilMat.materials[0].name == "Soil")
        {
            gridSoilMat.materials = new Material[1] { gridSoils[gridSoilsIndex] };
            gridSoilsIndex++;
        }
        else return;
    }
}
