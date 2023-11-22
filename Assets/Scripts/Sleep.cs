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
            //transform.GetChild(0).gameObject.SetActive(true);
            //transform.GetChild(0).gameObject.transform.LookAt(XROrigin);
            //transform.GetChild(0).gameObject.transform.forward *= -1;
            TimeManager.Instance.UpdateDay();
            print(TimeManager.Instance.Day);
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
