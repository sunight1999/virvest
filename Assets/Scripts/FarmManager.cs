using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public BoxCollider pitchForkCol; //밭갈개의 판정 콜라이더
    public int plowCount; //밭갈개의 땅 갈기 카운터

    public float moisture; //수분 측정을 위한 변수

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
