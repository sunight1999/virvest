using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "PitchFork_BoxCollider")
        {
            gameObject.transform.Find("Soil").gameObject.SetActive(true);
        }
    }
}