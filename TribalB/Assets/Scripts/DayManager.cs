using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{

    public int dayN;
    private float fadeDuration = 3.0f;

    public bool waitDay1;

    [SerializeField] CanvasGroup fade;

    [SerializeField] PlayerMovement player;

    private DayNight night;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        night = FindAnyObjectByType<DayNight>();
        gameManager = FindAnyObjectByType<GameManager>();
        dayN = 1;
        waitDay1 = true;

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
        Survivors();
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

    private void Survivors()
    {
        int survivors = gameManager.foodBase - gameManager.brimBrams.Count;

        if (survivors <0)
        {
            for(int i = 0; i < Mathf.Abs(survivors); i++)
            {
                Destroy(gameManager.brimBrams[gameManager.brimBrams.Count - 1]);
                gameManager.brimBrams.RemoveAt(gameManager.brimBrams.Count-1);
            }
        }
    }
}
