using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public BoxCollider pitchForkCol;

    public Ground ground;

    public int plowCount;
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
