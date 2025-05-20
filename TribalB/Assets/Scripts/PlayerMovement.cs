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

    private GameManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
    }


    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveX*speed, 0, moveZ*speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        switch(collision.gameObject.tag)
        {
            case "BrimBram":
                collision.gameObject.transform.SetParent(transform, true);
                Vector3 positonCollision = collision.transform.localPosition;
                gameObject.GetComponent<BoxCollider>().size += new Vector3(Math.Abs(collision.transform.localPosition.x), Math.Abs(collision.transform.localPosition.y), Math.Abs(collision.transform.localPosition.z));
                gameObject.GetComponent<BoxCollider>().center = positonCollision *0.5f;
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
                break;
        }
    }

}
