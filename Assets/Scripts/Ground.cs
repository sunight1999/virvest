using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ground : MonoBehaviour
{
    public FarmManager farmManager;

    public GameObject soil;
    public GameObject sharpenSoil;

    public List<GameObject> composts = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other == farmManager.pitchForkCol && transform.gameObject.layer == 10)
        {
            farmManager.plowCount++;
            Debug.Log(farmManager.plowCount);
            if (farmManager.plowCount > 2)
            {
                farmManager.plowCount = 0;
                Plow();
                foreach (GameObject c in composts)
                {
                    Destroy(c);
                }
            }
            farmManager.pitchForkCol.enabled = false;
        }
        else if (other.gameObject.tag == "FarmEquipment S" && transform.gameObject.layer == 11)
        {
            sharpenSoil.SetActive(true);
            soil.SetActive(false);
            this.gameObject.layer = 12;
            //Destroy(transform.GetChild(0).gameObject);
        }
    }

    void Plow()
    {
        soil.SetActive(true);
        transform.gameObject.layer = 11;
    }
}