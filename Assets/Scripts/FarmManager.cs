using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public GameObject compost;
    public ParticleSystem compostParticle;

    public GameObject Fertilizer;

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
        Composting();
        Debug.Log(compost.transform.eulerAngles);
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

    public void Composting()
    {
        if(isGrab && (compost.transform.eulerAngles.z > 160 && compost.transform.eulerAngles.z < 200))
        {
            compostParticle.Play();
        }
        else
            compostParticle.Stop();
    }
}
