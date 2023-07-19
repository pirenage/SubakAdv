using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using System;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDays = 86400f;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color sunRiseLightColor = new Color(1f, 0.75f, 0.5f); // Contoh warna fajar
    [SerializeField] Color nightLightColor = new Color(0.1f, 0.1f, 0.2f); // Contoh warna malam
    [SerializeField] Color sunSetLightColor = new Color(1f, 0.4f, 0.2f); // Contoh warna senja
    [SerializeField] Color dayLightColor = Color.white; // Contoh warna siang

    float time;
    [SerializeField] float timeScale = 60f;
    [SerializeField] TextMeshProUGUI Time;
    [SerializeField] TextMeshProUGUI Day;
    [SerializeField] Light2D globalLight;
    private int days;

    string[] dayNames = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu" };
    int currentDayIndex = 0;
    private int currentDate = 1;

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    void Update()
    {
        time += UnityEngine.Time.deltaTime * timeScale;
        int hh = (int)Hours;
        int mm = (int)Minutes;
        if (mm % 5 != 0)
        {
            mm = ((mm / 5) + 1) * 5;
            if (mm >= 60)
            {
                mm = 0;
                hh = (hh + 1) % 24;
            }
        }
        Time.text = hh.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c;
        if (v < 0.25f)
        {
            c = Color.Lerp(nightLightColor, sunRiseLightColor, v / 0.25f);
        }
        else if (v < 0.75f)
        {
            c = Color.Lerp(sunRiseLightColor, sunSetLightColor, (v - 0.25f) / 0.5f);
        }
        else
        {
            c = dayLightColor;
        }

        globalLight.color = c;

        int currentDay = (days + currentDayIndex) % 7; // Hitung indeks hari saat ini
        Day.text = dayNames[currentDay] + " , " + (currentDate + 1).ToString("00");

        if (time > secondsInDays)
        {
            nextDay();
        }
    }

    private void nextDay()
    {
        time = 0;
        days += 1;

        if (currentDate >= 30)
        {
            currentDate = 1;
        }
        else
        {
            currentDate++;
        }
    }
}
