using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            break;
        }

    }

}
