using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineControll : MonoBehaviour
{
    Material material;
    
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_OutlineWidth", 0);
    }

    public void OnOutline()
    {
        material.SetFloat("_OutlineWidth", 3);
    }

    public void OffOutline()
    {
        material.SetFloat("_OutlineWidth", 0);
    }
}
