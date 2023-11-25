using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public GameObject sleepUI;
    public GameObject badCam;
    [SerializeField] Transform XROrigin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin" && TimeManager.Instance.HourOfTime() >= 7 && TimeManager.Instance.MinOfTime() > 0)
        {
            sleepUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin" && TimeManager.Instance.HourOfTime() >= 7 && TimeManager.Instance.MinOfTime() > 0)
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
        TimeManager.Instance.UpdateDay();
        Invoke("fadeScreen.FadeOut()", 1f);
    }
}
