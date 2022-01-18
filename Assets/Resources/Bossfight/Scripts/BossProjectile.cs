using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger");
        if (other.CompareTag("Ground") || other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            print("Trigger");
        }
    }

}
