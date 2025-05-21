using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Original code by "Dev Ritch" Youtube Link: https://www.youtube.com/watch?v=BU5HVnMbqb8&list=WL&index=24

public class DayNight : MonoBehaviour
{
    [Range(0.0f, 24.0f)] public float dayTime = 12;

    public GameObject sun;

    public float dayDurationMin = 60;

    [SerializeField]public RawImage sunImage;

    private float sunX;
    private void Update()
    {
        dayTime += Time.deltaTime * (24 / (60 * dayDurationMin));
        if(dayTime >= 24)
        {
            dayTime = 6;
        }
        SunRotation();
       
        sunImage.rectTransform.transform.position = new Vector3(64.958f * dayTime, 84.9389f, 0);
    }

    private void SunRotation()
    {
        sunX = 15 * dayTime;

        sun.transform.localEulerAngles = new Vector3(sunX, 0, 0);

        if(dayTime < 6 || dayTime > 18)
        {
            sun.GetComponent<Light>().intensity = 0;
        }
        else
        {
            sun.GetComponent <Light>().intensity = 1;
        }
    }
}
