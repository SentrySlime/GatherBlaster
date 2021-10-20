using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_Projectile : MonoBehaviour
{

    public Rigidbody rb;
    void Start()
    {
        
    }

    void Update()
    {
        rb.AddForce(transform.right * 1.1f, ForceMode.Force);
    }
}
