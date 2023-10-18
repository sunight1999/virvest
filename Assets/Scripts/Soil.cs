using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private bool onShovel = false;
    GameObject onShovelPos = null;
    private void Update()
    {
        if (onShovel)
        {
            transform.position = onShovelPos.transform.position;
            transform.rotation = onShovelPos.transform.rotation;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Shovel_Boxcollider")
        {
            collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            onShovelPos = collider.gameObject.transform.GetChild(0).gameObject;
            onShovel =true;
            /*
            var colobj = collider.gameObject.transform.GetChild(0).gameObject.transform;
            Instantiate(gameObject, colobj.position, colobj.rotation, collider.gameObject.transform.parent);
            Destroy(gameObject);*/
            //transform.position = collider.gameObject.transform.GetChild(0).position;
        }
    }
}