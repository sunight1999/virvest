using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchForkSwing : MonoBehaviour
{
    public BoxCollider pitchForkCol;
    Rigidbody rb;
    bool isGrab;

    float timer = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        isGrab=true;
    }

    public void OffGrab()
    {
        isGrab=false;
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (isGrab && speed > 10 && timer < 0)
        {
            pitchForkCol.enabled = true;
            timer = 0.5f;
        }
        timer -= Time.deltaTime;
        if(timer < 0 || !isGrab)
        {
            pitchForkCol.enabled = false;
        }
    }
}
