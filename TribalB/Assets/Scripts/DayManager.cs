using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{

    public int dayN;
    private float fadeDuration = 3.0f;

    [SerializeField] CanvasGroup fade;

    [SerializeField] PlayerMovement player;

    private DayNight night;
    // Start is called before the first frame update
    void Start()
    {
        night = FindAnyObjectByType<DayNight>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.nightTime == true)
        {
            
            
            StartCoroutine("NextDay");
            player.nightTime = false;


        }
    }

    IEnumerator NextDay()
    {
        yield return StartCoroutine("FadeToBlack");
        night.dayTime = 6;
        yield return StartCoroutine("FadeToGame");
    }

    IEnumerator FadeToBlack()
    {
        float t = 0;
        while(t< fadeDuration)
        {
            t += Time.deltaTime;
            fade.alpha = Mathf.Lerp(0, 1, t/fadeDuration);
            yield return null;
        }
        fade.alpha =  1;
    }

    IEnumerator FadeToGame()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fade.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
        fade.alpha =  0;
    }
}
