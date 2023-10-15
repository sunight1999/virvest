using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchFork : MonoBehaviour
{
    public Ground ground;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grid")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = ground.soilMat;
        }
    }
}
