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
        //farmManager = GameObject.FindObjectOfType<FarmManager>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (farmManager.isGrab == true)
        {
            pitchForkCol.enabled = true;
        }
        else if(farmManager.isGrab == false)
        {
            pitchForkCol.enabled = false;
        }
    }
}
