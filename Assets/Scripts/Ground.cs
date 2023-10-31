using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static LocomotionTeleport;

public class Ground : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "PitchFork_BoxCollider")
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}