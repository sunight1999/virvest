using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject prefab;

    // �簥�Կ� ��ȣ�ۿ�� �ʿ��� ��Ʈ���� ����
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
