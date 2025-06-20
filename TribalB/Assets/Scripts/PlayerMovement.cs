using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] public float speed;

    [Header("RigidBody")]
    [SerializeField] public Rigidbody rb;

    [Header("Colider")]
    [SerializeField] public MeshCollider mesh;

    [Header("WASD")]
    [SerializeField] public RawImage wasd;

    [Header("RightClick")]
    [SerializeField] public RawImage rClick;

    [SerializeField] public GameObject hey;

    [SerializeField] public AudioSource beFriends;
    [SerializeField] public AudioSource leaveResources;
    [SerializeField] public AudioSource heyAudio;

    private GameManager manager;
    private DayNight dayNight;
    private DayManager dayManager;
    private ResorcesGenerate resorcesGenerate;
    private BrimBramGenerator brimBramGenerator;
    private Vector3 sizeAfterCollision;

    public bool nightTime;
    public bool safe;
    private bool moved;
    private bool lclickBool;

    private Vector3 StartPosition;



    private void Start()
    {
        nightTime = false;
        manager = FindAnyObjectByType<GameManager>();
        dayNight = FindAnyObjectByType<DayNight>();
        dayManager = FindAnyObjectByType<DayManager>();
        resorcesGenerate = FindAnyObjectByType<ResorcesGenerate>();
        brimBramGenerator = FindAnyObjectByType<BrimBramGenerator>();
        StartPosition = gameObject.transform.position;
        StartCoroutine("StartMoving");
        moved = false;
        lclickBool = false;
    }


    void Update()
    {
        if (!moved)
        {
            StartCoroutine("StartMoving");
        }
        else
        {
            StartCoroutine("RecolectWarning");
        }
        
        float moveX = Input.GetAxis("Vertical");
        float moveZ = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(-moveX*speed, 0, moveZ*speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        switch(collision.gameObject.tag)
        {
            case "BrimBram":
                collision.gameObject.transform.SetParent(transform, true);
                if(dayManager.waitDay1 == true)
                {
                    resorcesGenerate.GenerateResourcesRandom();
                    dayManager.waitDay1 = false;
                }
                manager.brimBrams.Add(collision.gameObject);
                beFriends.Play();

                //collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                //gameObject.GetComponent<BoxCollider>().size += new Vector3(Math.Abs(collision.transform.localPosition.x), Math.Abs(collision.transform.localPosition.y), Math.Abs(collision.transform.localPosition.z));
                //sizeAfterCollision = gameObject.GetComponent<BoxCollider>().size;
                //WhereCollide(collision.gameObject);
                break;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Base":
                manager.woodBase += manager.woodResources;
                manager.woodResources = 0;
                manager.stoneBase += manager.stoneResources;
                manager.stoneResources = 0;
                manager.foodBase += manager.foodResources;
                manager.foodResources = 0;
                leaveResources.Play();
                safe = true;
                if (dayNight.dayTime > 18)
                {
                    nightTime = true;
                }
                if (manager.firstRecources == true)
                {
                    manager.firstRecources = false;

                    heyNeighbour();

                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Base")
        {
            safe = false;
        }
    }

    private void WhereCollide(GameObject collision)
    {
        Vector3 newCenter = new Vector3(0,0,0);

        newCenter = gameObject.GetComponent<BoxCollider>().center + collision.transform.localPosition;

        newCenter = newCenter / 2;

        gameObject.GetComponent<BoxCollider>().center  = newCenter;

    }

    private IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(5f);
        if(StartPosition == gameObject.transform.position)
        {
            
            wasd.color += new Color(0,0,0,0.01f);
        }
        else
        {
            
            wasd.color = new Color(0, 0, 0, 0);
            moved = true;
            
            
        }
    }

    private IEnumerator RecolectWarning()
    {
        yield return new WaitForSeconds(5f);
        if (manager.stoneResources == 0 && manager.woodResources == 0 && manager.foodResources == 0)
        {
            if (lclickBool == false)
            {
                rClick.color += new Color(0, 0, 0, 0.01f);
            }
                
        }
        else
        {
            rClick.color = new Color(0, 0, 0, 0);
            lclickBool = true;

        }
    }

    private void heyNeighbour()
    {
        hey.SetActive(true);
        heyAudio.Play();

        brimBramGenerator.GenerateBrimBrams();
    }

}
