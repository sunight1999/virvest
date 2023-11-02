using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ground : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "PitchFork_BoxCollider" && transform.childCount > 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.tag = "Sharpen";
        }
        
        if(collider.gameObject.name == "Shovel_Boxcollider" 
            && transform.Find("soil_3m_mid"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Destroy(transform.GetChild(0).gameObject);
        }

    }
}