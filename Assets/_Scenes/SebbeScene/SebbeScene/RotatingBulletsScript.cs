using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBulletsScript : MonoBehaviour
{
    public float yAmount = 1;
    void Start()
    {
        
    }

    void Update()
    {
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        transform.Rotate(0, yAmount, 0, Space.Self);
    }
}
