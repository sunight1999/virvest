using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject housePanel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "XR Origin")
        {
            housePanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            housePanel.SetActive(false);
        }
    }

    public void OffPanel()
    {
        housePanel.SetActive(false);
    }

    public void SceneChange()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene(1);
        else if (SceneManager.GetActiveScene().buildIndex == 1) SceneManager.LoadScene(0);
    }
}
