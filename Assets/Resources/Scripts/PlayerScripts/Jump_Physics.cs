using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Physics : MonoBehaviour
{

    //private Rigidbody rb;

    //--------------
    ThirdPersonMovement playerMovement;
    CharacterController cc;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float defaultGravity;

    void Start()
    {
        playerMovement = GetComponent<ThirdPersonMovement>();
        defaultGravity = playerMovement.gravityMultiplier;
        cc = playerMovement.controller;
    }

    void Update()
    {


        if (cc.isGrounded)
            playerMovement.gravityMultiplier = defaultGravity;

        if(cc.velocity.y < 0)
        {
            print(1);
            playerMovement.gravityMultiplier -= Physics.gravity.y * (fallMultiplier - 2) * Time.deltaTime;

            
        }
        else if(cc.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            print(2);
            playerMovement.gravityMultiplier -= Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        //}
        //else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}
    }
}