using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelSwing : MonoBehaviour
{
    Rigidbody rb;

    float timer = 1;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (speed > 5f && transform.GetChild(1).GetChild(0).childCount > 0)
        {
            GameObject soil = transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            soil.gameObject.GetComponent<MeshCollider>().isTrigger = false;
            soil.AddComponent<Rigidbody>();
            soil.transform.SetParent(null);
            Destroy(soil, 10f);
            transform.GetChild(1).gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
