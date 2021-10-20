using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float runSpeeed;

    [Header("Directions")]
    public Vector3 moveDirection;
    public Vector3 lastDirection;

    [Header("Speeds")]
    public float turnSpeed = 1f;
    public float jumpSpeed = 10;
    public float fallSpeed = 10;


    float singleStep;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Quaternion lastQuaternion;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        lastDirection = Vector3.zero;
    }

    private void Update()
    {
        rb.AddForce(Vector3.down * fallSpeed);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity += Vector3.up * jumpSpeed;
            //rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }


        



    }

    public void FixedUpdate()
    {
        singleStep = turnSpeed * Time.deltaTime;
        
        MovementTwo();
        

    }


    public void MovementOne()
    {
        if (Input.anyKey)
        {

            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * movementSpeed;
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection.y = 0.0f;

            if (Input.GetMouseButton(1))
            {

                transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
                if (Input.GetKey(KeyCode.LeftShift))
                    rb.transform.position += moveDirection * runSpeeed;
                else
                    rb.transform.position += moveDirection;

            }
            else
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                if (Input.GetKey(KeyCode.LeftShift))
                    rb.transform.position += moveDirection * runSpeeed;
                else
                    rb.transform.position += moveDirection;
                lastDirection = moveDirection;
            }

        }
    }

    public void MovementTwo()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        
        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            //rb.velocity = moveDir.normalized * movementSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift))
                rb.transform.position += moveDir.normalized * runSpeeed * Time.deltaTime;

            else
                rb.transform.position += moveDir.normalized * movementSpeed * Time.deltaTime;
            print("Moving");
            lastQuaternion = transform.rotation;
        }
        else
        {
            print("Standing still");
            if (rb.transform.rotation != lastQuaternion)
                rb.transform.rotation = lastQuaternion;
            //rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

}