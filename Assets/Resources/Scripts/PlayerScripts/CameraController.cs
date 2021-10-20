using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public Vector2 pitchMinMax = new Vector2(-10, 70);

    public float rotationSmoothTime = .2f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public float dstFromTarget = 10;
    public float camOffset = 10;
    public GameObject player;


    float yaw;
    float pitch;

    private void Awake()
    {
        target = GameObject.Find("CameraTarget").GetComponent<Transform>();
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, target.position, 8))
        {
            //transform.position = 
        }
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        transform.eulerAngles = currentRotation;
        //transform.position = target.position - transform.forward * dstFromTarget;
        transform.position = target.position - transform.forward * dstFromTarget + transform.right * camOffset;

    }
}
