using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHouseDoor : MonoBehaviour, Openable
{
    private Animator anim;
    private bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
    }

    public void ToggleDoor()
    {
        Debug.Log("OpenClose");

        if (isOpen)
            Close();
        else
            Open();

        isOpen = !isOpen;
    }

    public void Open()
    {
        anim.SetTrigger("doorOpen");
    }

    public void Close()
    {
        anim.SetTrigger("doorClose");
    }
}
