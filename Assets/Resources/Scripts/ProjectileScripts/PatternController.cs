using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct BulletInformation
{
    public GameObject Target { get; set; }
    public Transform Barrel { get; set; }
    public GameObject Projectile { get; set; }
    public float ProjectileSpeed { get; set; }
    public float ProjectileTravelDistance { get; set; }
    public int Damage { get; set; }
    public float FireRate { get; set; }


    public BulletInformation(GameObject target, Transform barrel, GameObject projectile, float projectileSpeed, float projectileTravelDistance, int damage, float fireRate)
    {
        this.Target = target;
        this.Barrel = barrel;
        this.ProjectileSpeed = projectileSpeed;
        this.ProjectileTravelDistance = projectileTravelDistance;
        this.Damage = damage;
        this.FireRate = fireRate;
        this.Projectile = projectile;
    }
}
public class PatternController : MonoBehaviour
{
    //--STATS--//
    public BulletInformation bulletInformation;
    public BulletSpawner.BulletPatterns typeOfPattern;

    public Queue<GameObject> bullets = new Queue<GameObject>();

    public int maxAmountOfBullets = 30;

    public bool shoot = false;

    
    void Start()
    {
        if(bullets.Count == 0)
            CreateBullets(maxAmountOfBullets); //Fill the queue of bullets that will be used by this prefab.
    }

    private void CreateBullets(int amount)
    {
        GameObject bulletHolder = new GameObject("BulletHolder");
        bulletHolder.transform.SetParent(transform);
        bulletHolder.transform.localPosition = Vector3.zero;

        for (int i = 0; i < amount; i++)
        {
            if(bulletInformation.Projectile != null)
            {
                GameObject bullet = Instantiate<GameObject>(bulletInformation.Projectile);
                bullet.transform.SetParent(bulletHolder.transform);

                //ASSIGN DAMAGE HERE
                BasicProjectileScript basicProjectile;
                if (bullet.TryGetComponent<BasicProjectileScript>(out basicProjectile))
                {
                    basicProjectile.SetDamage(bulletInformation.Damage);
                    basicProjectile.playerProjectile = false; //set it to an enemyProjectile
                    basicProjectile.patternController = this; //This is needed for the bullet to return itself to queue
                    basicProjectile.SetTravelDistance(bulletInformation.ProjectileTravelDistance);
                }
                else
                    Debug.LogError("Projectileprefab used by pattern has no BasicProjectileScript assigned. Pattern: " + transform.name + " Projectile: " + bulletInformation.Projectile.name);

                bullet.SetActive(false);
                bullets.Enqueue(bullet);
            }
            else
            {
                Debug.LogError("bulletinformation was not correctly assigned. You could have a PatternController loose in the scene.");
            }
            
            
            
        }
    }
    public void ReturnBulletToQueue(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}
