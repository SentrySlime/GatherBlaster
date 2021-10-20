using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularShots : MonoBehaviour
{
    private PatternController patternController; //Holds all necessary stats
    BulletInformation bulletInformation;


    float timer;

    void Start()
    {
        patternController = GetComponent<PatternController>();
        bulletInformation = patternController.bulletInformation;
    }

    // Update is called once per frame
    void Update()
    {
        if(patternController.shoot)
        {
            if (timer <= bulletInformation.FireRate)
                timer += Time.deltaTime;

            if (timer >= bulletInformation.FireRate)
            {
                timer = 0;
                if (patternController.bullets.Count > 0)
                {
                    GameObject bullet = patternController.bullets.Dequeue();
                    bullet.transform.localPosition = Vector3.zero;
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    bullet.SetActive(true);
                    //Rigidbody rb = Instantiate(bulletInformation.projectile, barrels[i].position, barrels[i].rotation).GetComponent<Rigidbody>();

                    Vector3 dir = bulletInformation.Target.transform.position - transform.position;
                    //Vector3 dir = transform.position - bulletInformation.target;
                    rb.AddForce(dir * bulletInformation.ProjectileSpeed);
                }
                else
                    Debug.Log("Queue is empty.");
            }
        }
        
    }
}
