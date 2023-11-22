using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : SingletonMono<TimeManager>
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    [SerializeField] private Light sunLight;
    [SerializeField] private TextMeshProUGUI timeText;

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    //public static TimeManager Instance { get; private set; }
    public int Day { get; private set; }

    private void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(sunriseHour) + TimeSpan.FromDays(1f - DateTime.Today.Day);
        Day = currentTime.Day;

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        Instance.StartTime();
    }


    private IEnumerator TimeUpdate()
    {
        while (currentTime.TimeOfDay < sunsetTime && SceneManager.GetActiveScene().buildIndex == 1)
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
            if (timeText == null) timeText = GameObject.FindObjectOfType<TextMeshProUGUI>();

            UpdateTimeofDay();
            RotateSun();
            yield return null;
        }
    }

    private void UpdateTimeofDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if (timeText != null)
        {
            timeText.text = currentTime.ToString(" d") + " day " + currentTime.ToString("HH:mm");
        }
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
        if (timeText != null)
            timeText.text = currentTime.ToString(" d") + " day " + currentTime.ToString("HH:mm");
        Day = currentTime.Day;
        Grid.Instance.UpdateSeeding();
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