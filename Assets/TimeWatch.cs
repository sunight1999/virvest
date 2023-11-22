using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeWatch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;

    private void Update()
    {
        time.text = TimeManager.Instance.TimeWatch();
    }
}
