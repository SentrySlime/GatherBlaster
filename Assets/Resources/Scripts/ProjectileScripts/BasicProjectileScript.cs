using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileScript : MonoBehaviour
{
    public PatternController patternController;
    public bool playerProjectile = true;
    public float distanceToTravel = 7f;

    public float travelTimer;

    private Rigidbody rgb;

    //--STATS--//
    int damage = 0;

    private void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }
    public void SetTravelDistance(float value)
    {
        distanceToTravel = value;
        travelTimer = distanceToTravel;
    }
    void Update()
    {
        travelTimer -= Time.deltaTime;

        if (travelTimer <= 0f)
        {
            if (!playerProjectile)
            {
                travelTimer = distanceToTravel; //Reset timer to original value that was given at instantiation.
                rgb.velocity = Vector3.zero;
                patternController.ReturnBulletToQueue(this.gameObject);
            }
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerProjectile)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyHealth tempEnemy = other.gameObject.GetComponentInParent<EnemyHealth>();
                tempEnemy.ReduceHealth(3);
                gameObject.SetActive(false);

            }
        }
        else
        {
            //Damage player
            if(other.CompareTag("Player"))
            {
                PlayerHealth.Instance.ReduceHealth(damage);
                travelTimer = distanceToTravel; //Reset timer for next use;
                rgb.velocity = Vector3.zero;
                patternController.ReturnBulletToQueue(this.gameObject);
            }
            else if (other.CompareTag("Ground"))
            {
                travelTimer = distanceToTravel; //Reset timer for next use;
                rgb.velocity = Vector3.zero;
                patternController.ReturnBulletToQueue(this.gameObject);
            }
        }

        

    }
    public void SetDamage(int amount)
    {
        damage = amount;
    }

}
