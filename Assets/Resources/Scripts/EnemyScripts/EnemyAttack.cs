using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public BulletSpawner.BulletPatterns currentAttackPattern;

    [HideInInspector] public GameObject currentAttackPrefab; //Current attack pattern that has been given by the bulletspawner
    public PatternController currentPatternController;

    GameObject barrel;

    public GameObject projectile;
    public float fireRate = .08f;
    public float projectileSpeed = 100f;
    public float projectileTravelDistance = 2f;
    public int bulletDamage = 10;

    private void Awake()
    {
        barrel = transform.Find("Barrel").gameObject;
        //RequestNewAttackPrefab(gameObject.transform.position);
    }

    void Update()
    {
        
    }

    public void RequestNewAttackPrefab(GameObject target)
    {
        //Before calling the method for our pattern of choice we need to correctly set the bulletinformation it needs.
        BulletInformation bulletInformation = new BulletInformation(target, barrel.transform, projectile, projectileSpeed, projectileTravelDistance, bulletDamage, fireRate);

        //call correct shooting pattern here. AI should probably determine what that is before calling this method at all.
        //Bulletspawner "lends" us a prefab to use until we don't need it anymore. This could either be from lost aggro or death.
        currentAttackPrefab = BulletSpawner.Instance.SpawnPattern(currentAttackPattern);

        if(currentAttackPrefab != null)
        {
            currentAttackPrefab.transform.SetParent(barrel.transform); //Make it follow the enemy position.
            currentAttackPrefab.transform.localPosition = Vector3.zero;


            //---SET ALL STATS HERE--//
            if(currentAttackPrefab.TryGetComponent<PatternController>(out currentPatternController))
            {
                currentPatternController.bulletInformation = bulletInformation; //Give pattern all our specified stats to use.
            }
            else
            {
                Debug.LogError("No patterncontroller attached to attackPatternPrefab: " + currentAttackPrefab);
            }
            
            currentAttackPrefab.SetActive(true);
        }
        
    }
    public void Shoot(bool shoot)
    {
        if(currentAttackPrefab != null)
        {
            if (shoot)
                currentPatternController.shoot = true;
            else
                currentPatternController.shoot = false;
        }
        
    }
    public void ReturnCurrentAttackPrefab()
    {
        BulletSpawner.Instance.ReturnPatternToQueue(currentAttackPrefab, currentPatternController.typeOfPattern);
    }
    
    
}
