using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    [SerializeField] Transform XROrigin;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "XR Origin")
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
