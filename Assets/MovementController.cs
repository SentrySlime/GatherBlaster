using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Transform cam;
    PlayerInput playerInput;
    CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;

    //Gravity
    float gravity = -8.8f;
    float groundedGravity = -0.05f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    bool isJumping = false;

    [Header("Walk / Run")]
    public float walkSpeed = 2.0f;
    public float runmultiplier = 3.0f;
    public float rotationFactorPerFrame = 1.0f;

    [Header("Jump")]
    public float maxJumpHeight = 10f;
    public float maxJumpTime = 5f;
    public float maxFallSpeed = 40f;


    private void Awake()
    {
        cam = Camera.main.transform;

        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;
        SetupJumpVariables();

    }

    void Update()
    {
        HandleRotation();
        OnMovement();
      
        HandleGravity();
        HandleJump();
    }


    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        
    }

    void HandleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            appliedMovement.y = initialJumpVelocity;
        }
        else if(!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2f;
        if(characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        }
        else if(isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * .5f, -maxFallSpeed);

        }
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * .5f;

        }
    }

    void OnRun (InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void OnMovement()
    {
        print("Hi");
        currentMovement.x = currentMovementInput.x * walkSpeed;
        currentMovement.z = currentMovementInput.y * walkSpeed;
        currentRunMovement.x = currentMovementInput.x * runmultiplier;
        currentRunMovement.z = currentMovementInput.y * runmultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

        Transform tempTrans = Camera.main.transform;
        tempTrans.position = new Vector3(0, Camera.main.transform.position.y, 0);
        tempTrans.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);

        currentMovement = tempTrans.TransformDirection(currentMovement);
        //currentMovement = Camera.main.transform.TransformDirection(currentMovement);
        currentRunMovement = tempTrans.TransformDirection(currentRunMovement);

        if (isRunPressed)
        {

            appliedMovement.x = currentRunMovement.x;
            appliedMovement.z = currentRunMovement.z;
        }
        else
        {
            appliedMovement.x = currentMovement.x;
            appliedMovement.z = currentMovement.z;
        }

        characterController.Move(appliedMovement * Time.deltaTime);

    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        
        currentMovementInput = context.ReadValue<Vector2>();
        //currentMovement.x = currentMovementInput.x * walkSpeed;
        //currentMovement.z = currentMovementInput.y * walkSpeed;
        //currentRunMovement.x = currentMovementInput.x * runmultiplier;
        //currentRunMovement.z = currentMovementInput.y * runmultiplier;
        //isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;


        //float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

    }

    private void OnEnable()
    {
        print("Enabled");
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

}
