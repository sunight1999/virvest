using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public GameObject sleepUI;
    [SerializeField] Transform XROrigin;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "XR Origin")
        {
            sleepUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            sleepUI.SetActive(false);
        }
    }
}
