using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public BoxCollider pitchForkCol;
    Rigidbody rb;

    float timer = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (speed > 5 && timer < 0)
        {
            pitchForkCol.enabled = true;
            timer = 1;
        }
        timer -= Time.deltaTime;
        if(timer < 0 )
        {
            pitchForkCol.enabled = false;
        }
    }
}
