using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public GameObject projectile;

    public GameObject tentacleOne;
    public GameObject tentacleTwo;

    public GameObject player;

    public float velocity;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKeyDown)
            RapidFire(tentacleOne.transform);
    }

    public void RapidFire(Transform tentacleTransform)
    {
        GameObject tempPro = Instantiate(projectile, tentacleTransform.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - tentacleTransform.position).normalized;
        tempPro.GetComponent<Rigidbody>().AddForce(direction * velocity);
        
    }

}
