using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    [SerializeField] private Light sunLight;
    //[SerializeField] private TextMeshProUGUI timeText;

    private DateTime currentTime;
    private DateTime defaltTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    public static TimeManager Instance { get; private set; }
    public int Day { get; private set; }
    public bool isDestroyProtected;
    private void Awake()
    {
        if(Instance != null)
        {
            Instance.StartTime();
            Destroy(gameObject);
        } 
        else Instance = this;

        if (isDestroyProtected)
            DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        defaltTime = DateTime.Now.Date + TimeSpan.FromHours(sunriseHour) + TimeSpan.FromDays(1f - DateTime.Today.Day);
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(sunriseHour) + TimeSpan.FromDays(1f - DateTime.Today.Day);
        Day = currentTime.Day;

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        Instance.StartTime();
    }

    private IEnumerator TimeUpdate()
    {
        while (currentTime.TimeOfDay < sunsetTime && SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (sunLight == null)
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        sunLight = light;
                        break;
                    }
                }
            }

            UpdateTimeofDay();
            RotateSun();
            yield return null;
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(200, Vector3.right);
        sunLight.color = Color.blue;
    }

    private void UpdateTimeofDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
    }

    private void RotateSun()
    {
        TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
        TimeSpan timeSinceSurise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

        double percentage = timeSinceSurise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
        float sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    public void UpdateDay()
    {
        currentTime = currentTime.AddDays(1f);
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(sunriseHour) + TimeSpan.FromDays(currentTime.Day - DateTime.Today.Day);
        Day = currentTime.Day;
        Grid.Instance.UpdateSeeding();
    }
    public bool isFirst()
    {
        return currentTime == defaltTime;
    }
    public bool isDay()
    {
        return currentTime.TimeOfDay < sunsetTime;
    }
    public string TimeWatch()
    {
        return currentTime.ToString(" d") + " day " + currentTime.ToString("HH:mm");
    }
    public int HourOfTime()
    {
        return currentTime.Hour;
    }
    public int DayOfTime()
    {
        return currentTime.Day;
    }
    public int MinOfTime()
    {
        return currentTime.Minute;
    }
    public void StartTime()
    {
        StartCoroutine(TimeUpdate());
    }
    public void StopTime()
    {
        StopCoroutine(TimeUpdate());
    }
}