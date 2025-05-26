using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] public float speed;

    [Header("RigidBody")]
    [SerializeField] public Rigidbody rb;

    [Header("Colider")]
    [SerializeField] public MeshCollider mesh;

    private GameManager manager;
    private DayNight dayNight;
    DayManager dayManager;
    private Vector3 sizeAfterCollision;

    public bool nightTime;

    private void Start()
    {
        nightTime = false;
        manager = FindAnyObjectByType<GameManager>();
        dayNight = FindAnyObjectByType<DayNight>();
        dayManager = FindAnyObjectByType<DayManager>();
    }


    void Update()
    {
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
                    dayManager.waitDay1 = false;
                }
                manager.brimBrams.Add(collision.gameObject);

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
                if (dayNight.dayTime > 18)
                {
                    nightTime = true;
                }
                break;
        }
    }

    private void WhereCollide(GameObject collision)
    {
        Vector3 newCenter = new Vector3(0,0,0);

        newCenter = gameObject.GetComponent<BoxCollider>().center + collision.transform.localPosition;

        newCenter = newCenter / 2;

        gameObject.GetComponent<BoxCollider>().center  = newCenter;

    }

}
