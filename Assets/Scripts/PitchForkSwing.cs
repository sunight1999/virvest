using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchForkSwing : MonoBehaviour
{
    public FarmManager farmManager;
    public BoxCollider pitchForkCol;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (farmManager.isGrab == true && speed > 10)
        {
            pitchForkCol.enabled = true;
        }
        else if(farmManager.isGrab == false)
        {
            pitchForkCol.enabled = false;
        }
    }
}
