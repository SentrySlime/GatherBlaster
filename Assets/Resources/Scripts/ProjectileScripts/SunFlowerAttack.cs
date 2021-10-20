using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerAttack : MonoBehaviour
{
    private PatternController patternController; //Holds all necessary stats
    BulletInformation bulletInformation;

    public List<Transform> barrels = new List<Transform>();

    //float fireRate = 0.2f;
    float timer;


    void Start()
    {
        patternController = GetComponent<PatternController>();
        bulletInformation = patternController.bulletInformation;
    }

    
    void Update()
    {
        if(patternController.shoot)
        {
            if (timer <= bulletInformation.FireRate)
                timer += Time.deltaTime;

            if (timer >= bulletInformation.FireRate)
            {
                timer = 0;
                if (patternController.bullets.Count >= barrels.Count)
                    for (int i = 0; i < barrels.Count; i++)
                    {

                        GameObject bullet = patternController.bullets.Dequeue();
                        Rigidbody rb = bullet.GetComponent<Rigidbody>();
                        bullet.transform.position = barrels[i].position;
                        bullet.transform.rotation = barrels[i].rotation;
                        bullet.SetActive(true);
                        //Rigidbody rb = Instantiate(bulletInformation.projectile, barrels[i].position, barrels[i].rotation).GetComponent<Rigidbody>();
                        rb.AddForce(barrels[i].forward * bulletInformation.ProjectileSpeed * 50);

                    }
                else
                    Debug.Log("Queue is empty.");
            }
        }
        
    }
}
