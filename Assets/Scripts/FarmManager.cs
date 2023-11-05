using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public BoxCollider pitchForkCol; //�簥���� ���� �ݶ��̴�
    public int plowCount; //�簥���� �� ���� ī����

    public float moisture; //���� ������ ���� ����

    public Ground ground;

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
}
