using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelSwing : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider bc;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        bc = transform.GetChild(0).gameObject.GetComponent<BoxCollider>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        if (transform.GetChild(0).GetChild(0).childCount == 0) bc.enabled = true;
        else
        {
            if(speed > 5f)
            {
                GameObject soil = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                soil.GetComponent<MeshCollider>().isTrigger = false;
                soil.transform.SetParent(null);

                if (soil.GetComponent<Rigidbody>() == null) soil.AddComponent<Rigidbody>();
                    Destroy(soil, 2f);

                bc.enabled = true;
            }
            else bc.enabled = false;
        }
    }
}
