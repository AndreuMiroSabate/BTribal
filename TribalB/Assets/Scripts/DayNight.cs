using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Original code by "Dev Ritch" Youtube Link: https://www.youtube.com/watch?v=BU5HVnMbqb8&list=WL&index=24

public class DayNight : MonoBehaviour
{
    [Range(0.0f, 24.0f)] public float dayTime = 10;

    public GameObject sun;

    public float dayDurationMin = 60;

    [SerializeField]public RawImage sunImage;
    [SerializeField] public Image timeLine;

    private DayManager dayManager;

    private float sunX;

    private void Start()
    {
        dayManager = FindAnyObjectByType<DayManager>();
    }
    private void Update()
    {
        if (dayManager.waitDay1 == false)
        {
            dayTime += Time.deltaTime * (24 / (60 * dayDurationMin));
            if (dayTime < 24)
            {
                SunRotation();
                sunImage.rectTransform.transform.position = new Vector3(125 + 86.61f * (dayTime - 6), 84.9389f, 0);
                timeLine.gameObject.SetActive(true);

            }
        }
        else
        {
            SunRotation();
            sunImage.rectTransform.transform.position = new Vector3(125 + 86.61f * (dayTime - 6), 84.9389f, 0);
        }

        
    }

    private void SunRotation()
    {
        sunX = 15 * dayTime;

        sun.transform.localEulerAngles = new Vector3(sunX, 0, 0);

        if(dayTime < 6 || dayTime > 19)
        {
            sun.GetComponent<Light>().intensity = 0;
        }
        else
        {
            sun.GetComponent <Light>().intensity = 1;
        }
    }
}
