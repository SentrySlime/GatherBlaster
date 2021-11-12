using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public static ThirdPersonMovement Instance { get; private set; }

    public CharacterController controller;
    public Transform cam;

    public float speed = 6;
    public float sprintSpeed = 12;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Gravity")]
    public float jumpSpeed = 8;
    public float gravityEffect = 9.8f;

    public float gravityMultiplier;

    public float vSpeed = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {


        //Get axises 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //Check if character controller is grounded
        if(controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //if you are grounded and you press space you apply speed to the axis.y
                vSpeed = jumpSpeed;
            }
            else
                //if the character is grounded that speed is 0
                vSpeed = 0;
        }

        //And if the controller is in the air we apply "gravity" and move the controller downwards

        //vSpeed -= gravityEffect * Time.deltaTime;

        vSpeed += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        Vector3 tempgrav = new Vector3(direction.x, vSpeed, direction.z);
        controller.Move(tempgrav * Time.deltaTime);

        //If we get player input from either vertical or horizontal axis
        if (direction.magnitude >= 0.1f)
        {
            //Calculate in what direction to turn in relation to current camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            //Here we also apply the gravity to when the player is moving with (W, A, S, D)
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 tempMove = new Vector3(moveDir.x * sprintSpeed, vSpeed / sprintSpeed, moveDir.z * sprintSpeed);
                controller.Move(tempMove * Time.deltaTime);
            }
            else
            {
                Vector3 tempMove = new Vector3(moveDir.x * speed, vSpeed / speed, moveDir.z * speed);
                controller.Move(tempMove * Time.deltaTime);
            }
            

        }

    }
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    void HandleGravity()
    {

    }

}
