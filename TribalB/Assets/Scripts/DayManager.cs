using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DayManager : MonoBehaviour
{

    public int dayN;
    private float fadeDuration = 3.0f;

    public bool waitDay1;

    [SerializeField] CanvasGroup fade;

    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject monster;

    [SerializeField] GameObject BrimBram;

    private DayNight night;
    private GameManager gameManager;
    private ResorcesGenerate generate;
    private BrimBramGenerator generator;

    private bool nextDayStarted;
    // Start is called before the first frame update
    void Start()
    {
        night = FindAnyObjectByType<DayNight>();
        gameManager = FindAnyObjectByType<GameManager>();
        generate = FindAnyObjectByType<ResorcesGenerate>();
        generator = FindAnyObjectByType<BrimBramGenerator>();
        dayN = 1;
        waitDay1 = true;
        nextDayStarted = false;
        monster.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(night.dayTime >= 19)
        {
            MonsterTime();
        }
        
        if (player.nightTime == true && !nextDayStarted)
        {
            nextDayStarted=true;
            
            StartCoroutine("NextDay");
            


        }
        if (dayN >= 4)
        {
            SceneManager.LoadScene("Forms");
        }
    }

    IEnumerator NextDay()
    {
        
        monster.SetActive(false);
        yield return StartCoroutine("FadeToBlack");
        Survivors();
        ImproveBase();
        gameManager.foodBase -= gameManager.brimBrams.Count;
        monster.gameObject.transform.position = new Vector3(-69, 0, 90);
        player.gameObject.transform.position = new Vector3(0, 0.5f, 3);
        generate.EliminateResources();
        generate.GenerateResourcesRandom();
        generator.EliminateBrimBrams();
        generator.GenerateBrimBramsRandom();

        night.dayTime = 7;
        yield return StartCoroutine("FadeToGame");
        player.nightTime = false;
        nextDayStarted = false;
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
        else
        {
            Reproduction();
        }
    }

    private void MonsterTime()
    {
        monster.SetActive(true);
    }

    private void ImproveBase()
    {
        if(gameManager.necessaryWood <= gameManager.woodBase && gameManager.necessaryStone <= gameManager.stoneBase)
        {
            gameManager.woodBase -= gameManager.necessaryWood;
            gameManager.stoneBase -= gameManager.necessaryStone;
            gameManager.Base.transform.localScale += new Vector3(5, 0, 5);
            gameManager.necessaryWood += 5;
            gameManager.necessaryStone += 5;
        }
    }

    private void Reproduction()
    {
        for(int i = 1; i <= gameManager.brimBrams.Count; i++)
        {
            if (i%2 == 0)
            {
                GameObject newResource = Instantiate(BrimBram, player.gameObject.transform.position, new Quaternion(0, 0, 0, 1f));
                float x = Random.Range(player.gameObject.transform.localPosition.x-0.5f, player.gameObject.transform.localPosition.x + 0.5f);
                float z = Random.Range(player.gameObject.transform.localPosition.z - 0.5f, player.gameObject.transform.localPosition.z + 0.5f);
                Vector3 position = new Vector3(x, 0, z);

                newResource.transform.position = position;
                gameManager.brimBrams.Add(newResource);
                newResource.transform.SetParent(player.gameObject.transform, false);
            }
            
        }
    }
}
