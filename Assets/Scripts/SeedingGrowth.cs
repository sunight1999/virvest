using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedingGrowth : MonoBehaviour
{

    [SerializeField] private List<GameObject> seedingStep;

    public static SeedingGrowth Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void UpdateSeeding()
    {
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(seedingStep[TimeManager.Instance.Day - 1], transform.position, Quaternion.identity, transform);
    }


}
