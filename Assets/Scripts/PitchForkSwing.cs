using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchForkSwing : MonoBehaviour
{
    public FarmManager farmManager;
    public BoxCollider pitchForkCol;
    Rigidbody rb;

    float timer = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (farmManager.isGrab && speed > 5 && timer < 0)
        {
            pitchForkCol.enabled = true;
            timer = 0.3f;
        }
        timer -= Time.deltaTime;
        if(timer < 0 || farmManager.isGrab == false)
        {
            pitchForkCol.enabled = false;
        }
    }
}
