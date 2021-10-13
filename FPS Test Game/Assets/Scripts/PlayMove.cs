using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMove : MonoBehaviour
{
    //controller object reference
    public CharacterController controller;

    //forces acting on object
    public float gravity = -9.81f;
    public float speed = 12f;
    public float jumpHeight = 3f;

    //objects and values for checking ground referance
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //values for checking controller is grounded
    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {

        //checks with gameobject if colliding with any object labelled under groundmask
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //check if player is grounded and adjusts downwards velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //get horizontal and vertical keys as input
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        //applys left and right force to player object
        Vector3 move = transform.right * xMove + transform.forward * zMove;
        controller.Move(move * speed * Time.deltaTime);

        //adds upward force if spacebar is pressed and player is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * gravity * -2f);
        }

        //make upward force and apply force to player object
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
