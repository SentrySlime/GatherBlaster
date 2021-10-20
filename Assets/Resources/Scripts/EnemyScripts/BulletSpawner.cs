using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    private static BulletSpawner _instance;
    public static BulletSpawner Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Bulletspawner is null");

            return _instance;
        }
    }

    private Queue<GameObject> sunflowerQueue = new Queue<GameObject>();
    private Queue<GameObject> regularShotsQueue = new Queue<GameObject>();

    [SerializeField] GameObject sunflowerPrefab;
    [SerializeField] GameObject regularShotsPrefab;

    GameObject sunflowerParent;
    GameObject regularshotsParent;

    public int maxNumOfPatternsInScene = 50;

    public enum BulletPatterns
    {
        //OutwardCircleBurst,
        //InwardCircleBurst
        Sunflower,
        RegularShots
    }

    private void Awake()
    {
        _instance = this;
        InstantiateAllQueues();
    }
    private void InstantiateAllQueues()
    {
        //Create parent objects for each pattern to keep things neat and tidy
        sunflowerParent = new GameObject();
        sunflowerParent.transform.SetParent(transform);
        sunflowerParent.transform.name = "SunflowerPatterns";

        regularshotsParent = new GameObject();
        regularshotsParent.transform.SetParent(transform);
        regularshotsParent.transform.name = "RegularShots";

        //Instantiate all patterns, add to queue and deactivate
        for (int i = 0; i < maxNumOfPatternsInScene; i++)
        {
            //--Sunflower pattern--//
            GameObject pattern = Instantiate(sunflowerPrefab);
            pattern.transform.SetParent(sunflowerParent.transform);
            sunflowerQueue.Enqueue(pattern);
            pattern.SetActive(false);

            //--Regularshots pattern--//
            GameObject pattern2 = Instantiate(regularShotsPrefab);
            pattern2.transform.SetParent(regularshotsParent.transform);
            regularShotsQueue.Enqueue(pattern2);
            pattern2.SetActive(false);
        }
    }

    public void ReturnPatternToQueue(GameObject pattern, BulletPatterns typeOfPattern)
    {
        switch(typeOfPattern)
        {
            case BulletPatterns.Sunflower:
                sunflowerQueue.Enqueue(pattern);
                pattern.transform.SetParent(sunflowerParent.transform);
                pattern.SetActive(false);
                break;

            case BulletPatterns.RegularShots:
                regularShotsQueue.Enqueue(pattern);
                pattern.transform.SetParent(regularshotsParent.transform);
                pattern.SetActive(false);
                break;
        }
    }

    public GameObject SpawnPattern(BulletPatterns pattern)
    {
        GameObject objPattern = null;
        switch(pattern)
        {
            //case BulletPatterns.OutwardCircleBurst:
            //    OutwardCircleBurst(bulletInformation);
            //    break;

            //case BulletPatterns.InwardCircleBurst:
            //    InwardCircleBurst(bulletInformation);
            //    break;

            case BulletPatterns.Sunflower:
                objPattern = GetSunFlowerPattern();
                break;

            case BulletPatterns.RegularShots:
                objPattern = GetRegularShots();
                break;

        }

        return objPattern;
    }
    private GameObject GetSunFlowerPattern()
    {
        GameObject sunflower;
        if (sunflowerQueue.Count > 0)
        {
            sunflower = sunflowerQueue.Dequeue();
            return sunflower;
        }
        else
        {
            Debug.Log("No more bullet patterns in queue of type: Sunflower.");
            return null;
        }
        
    }
    private GameObject GetRegularShots()
    {
        GameObject regularShots;
        if (regularShotsQueue.Count > 0)
        {
            regularShots = regularShotsQueue.Dequeue();
            return regularShots;
        }
        else
        {
            Debug.Log("No more bullet patterns in queue of type: Regularshots.");
            return null;
        }
    }
    #region unused patterns
    //private void OutwardCircleBurst(BulletInformation bulletInformation)
    //{
    //    int bulletAmount = 1; //Amount of bullets to shoot in this pattern.

    //    //A point above the target.
    //    Vector3 startingPoint = bulletInformation.target + new Vector3(0, 3f, 0);

    //    for (int i = 0; i < bulletAmount; i++)
    //    {
    //        GameObject bullet = Instantiate(bulletInformation.prefab, bulletInformation.barrel.position, Quaternion.identity);

    //        //Set stats of bullet
    //        BasicProjectileScript proj;
    //        if (bullet.TryGetComponent<BasicProjectileScript>(out proj))
    //            proj.SetDamage(bulletInformation.damage);
    //        else
    //            Debug.Log("Projectile script missing!");

    //        bullet.transform.LookAt(bulletInformation.target);
    //        Vector3 direction = startingPoint - bulletInformation.target; //direction relative to pivot
    //        Vector3 angles = new Vector3(0, 0, 75f); //Rotate around what axis and how much?
    //        direction = Quaternion.Euler(angles) * direction; //Rotate

    //        Vector3 point = direction + bulletInformation.target;
    //        startingPoint = point;

    //        Vector3 dir = (point - bulletInformation.barrel.position).normalized;

    //        //startingPoint = Quaternion.Euler(60, 0, 0) * Vector3.forward;
    //        //starting


    //        //Add force in direction
    //        bullet.GetComponent<Rigidbody>().AddForce(dir * bulletInformation.projectileSpeed, ForceMode.Impulse);

            
    //    }

        
    //}
    //private void InwardCircleBurst(BulletInformation bulletInformation)
    //{
    //    int bulletAmount = 5; //Amount of bullets to shoot in this pattern.

    //    //A point above the barrel.
    //    Vector3 startingPoint = bulletInformation.barrel.position + new Vector3(0, 3f, 0);

    //    for (int i = 0; i < bulletAmount; i++)
    //    {
    //        Vector3 direction = startingPoint - bulletInformation.barrel.position; //direction relative to pivot
    //        Vector3 angles = new Vector3(0, 0, 75f); //Rotate around what axis and how much?
    //        direction = Quaternion.Euler(angles) * direction; //Rotate

    //        Vector3 point = direction + bulletInformation.barrel.position;
    //        startingPoint = point;

    //        Vector3 dir = (bulletInformation.target - point).normalized;

    //        GameObject bullet = Instantiate(bulletInformation.prefab, point, Quaternion.identity);
    //        //Set stats of bullet
    //        BasicProjectileScript proj = bullet.GetComponent<BasicProjectileScript>();
    //        proj.SetDamage(bulletInformation.damage);

    //        bullet.GetComponent<Rigidbody>().AddForce(dir * bulletInformation.projectileSpeed, ForceMode.Impulse);
    //    }
    //}
    #endregion
}
