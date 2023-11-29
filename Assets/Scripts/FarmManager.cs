using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmManager : MonoBehaviour
{
    public GameObject Fertilizer;

    public BoxCollider pitchForkCol; //�簥���� ���� �ݶ��̴�
    [SerializeField] public int plowCount; //�簥���� �� ���� ī����

    public Material[] gridSoils; //�� �־��� �� ���� ��Ʈ����
    [SerializeField] int gridSoilsIndex = 0;

    [SerializeField] public Ground farmland;

    public bool isGrab = true;

    //public static FarmManager instance;

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else instance = this;

    //    DontDestroyOnLoad(gameObject);
    //}

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
