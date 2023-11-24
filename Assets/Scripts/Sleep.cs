using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public GameObject sleepUI;
    public GameObject badCam;
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
            OffSleepPanel();
        }
    }

    public void OffSleepPanel()
    {
        sleepUI.SetActive(false);
    }

    public void OnSleep()
    {
        badCam.SetActive(true);
        OffSleepPanel();
        Camera.main.gameObject.SetActive(false);
    }
}
