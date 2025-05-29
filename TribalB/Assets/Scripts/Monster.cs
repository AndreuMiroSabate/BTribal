using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    private DayNight dayManager;
    private GameManager gameManager;
    private PlayerMovement player;

    private Vector3 directionMonster;
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        gameManager = FindAnyObjectByType<GameManager>();
        dayManager = FindAnyObjectByType<DayNight>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dayManager.dayTime >= 19 && player.safe == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.gameObject.transform.position, 0.02f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BrimBram")
        {
            StartCoroutine("afterEating");
            Destroy(gameManager.brimBrams[gameManager.brimBrams.Count - 1]);
            gameManager.brimBrams.RemoveAt(gameManager.brimBrams.Count - 1);
        }
    }

    IEnumerator afterEating()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        player.safe = true;
        yield return new WaitForSeconds(10f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        player.safe = false;

    }
}
