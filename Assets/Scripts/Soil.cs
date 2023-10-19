using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Shovel_Boxcollider")
        {            
            var shovelPos = collider.transform.GetChild(0).transform;
            transform.position = shovelPos.position;
            transform.rotation = shovelPos.rotation;
            transform.SetParent(shovelPos);
        }
    }
}