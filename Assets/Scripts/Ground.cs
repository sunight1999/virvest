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
        if (other.gameObject.tag == "FarmEquipment P" && transform.gameObject.layer == 9)
        {
            farmManager.plowCount++;
            if (farmManager.plowCount > 2 && transform.childCount > 1)
            {
                Plow();
                farmManager.plowCount = 0;
            }
            farmManager.pitchForkCol.enabled = false;
        }
        else if (other.gameObject.tag == "FarmEquipment S" && transform.Find("soil_3m_mid") && transform.gameObject.layer == 10)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void Plow()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.gameObject.layer = 10;
    }
}