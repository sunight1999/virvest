using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            print(other.gameObject.name);
            TimeManager.Instance.UpdateDay();
        }
    }
}
