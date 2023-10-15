using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject prefab;

    // 밭갈게와 상호작용시 필요한 메트리얼 변수
    public Material soilMat;

    bool isObj = false;
    void OnTriggerEnter(Collider collision)
    {
        //gameObject.transform.Find("Soil").gameObject.SetActive(true);
        /*
        if (collision.gameObject.name == "PitchFork_BoxCollider")
        {
            Instantiate(prefab, transform.position, Quaternion.identity, transform);
        }*/
    }
}
