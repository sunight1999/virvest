using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ground : MonoBehaviour
{
    public FarmManager farmManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PitchFork_BoxCollider" && transform.tag == "Farmland")
        {
            farmManager.plowCount++;
            Debug.Log(farmManager.plowCount);
            if (farmManager.plowCount > 2 && transform.childCount > 1)
            {
                Plow();
                farmManager.plowCount = 0;
            }
            farmManager.pitchForkCol.enabled = false;
        }

        if (other.gameObject.name == "Shovel_BoxCollider" && transform.Find("soil_3m_mid"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    void Plow()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.tag = "Sharpen";
    }
}