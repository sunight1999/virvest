using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineControll : MonoBehaviour
{
    Material material;
    FarmManager fm;
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_OutlineWidth", 0);
        fm = FindAnyObjectByType<FarmManager>();
    }

    public void OnOutline()
    {
        material.SetFloat("_OutlineWidth", GlobalDefines.outlineWidth);
    }

    public void OffOutline()
    {
        material.SetFloat("_OutlineWidth", 0);
    }

    private void Update()
    {
        if (fm.isGrab)
            OffOutline();
    }
}
