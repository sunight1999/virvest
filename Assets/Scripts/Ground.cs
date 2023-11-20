using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class Ground : MonoBehaviour
{
    public static Ground Instance { get; private set; }
    public FarmManager farmManager;

    public GameObject soil;
    public GameObject sharpenSoil;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FarmEquipment P" && transform.gameObject.layer == 9)
        {
            farmManager.plowCount++;
            Debug.Log(farmManager.plowCount);
            if (farmManager.plowCount > 2)
            {
                farmManager.plowCount = 0;
                Plow();
            }
            farmManager.pitchForkCol.enabled = false;
        }
        else if (other.gameObject.tag == "FarmEquipment S" && transform.gameObject.layer == 10)
        {
            sharpenSoil.SetActive(true);
            soil.SetActive(false);
            this.gameObject.layer = 11;
            //Destroy(transform.GetChild(0).gameObject);
        }
    }

    void Plow()
    {
        soil.SetActive(true);
        transform.gameObject.layer = 10;
    }
}