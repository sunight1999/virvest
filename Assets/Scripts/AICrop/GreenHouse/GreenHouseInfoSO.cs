using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenHouseInfo
{
    public string predictDate;
    public float predictValue;

    public GreenHouseInfo(string date, float value)
    {
        predictDate = date;
        predictValue = value;
    }
}

[CreateAssetMenu(fileName = "GreenHouseInfoSO", menuName = "GreenHouse/Info")]
public class GreenHouseInfoSO : ScriptableObject
{
    public List<string> predictDates;
    public List<float> predictValues;
}
