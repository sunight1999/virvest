using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "PitchFork_Pivot") Destroy(gameObject);
    }
}
