using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject projectile;

    [Header("barrels")]
    public GameObject tentacleOne;
    public GameObject tentacleTwo;
    public GameObject head;

    public GameObject player;

    public float velocity;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;


    void Start()
    {

    }

    void Update()
    {
        gameObject.transform.LookAt(player.transform.position);

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            //RapidFire(tentacleOne.transform);
            //RapidFire(tentacleTwo.transform);
            //print("Timeine");

            Shotgun();
        }

        //if (Input.anyKeyDown)
    }

    public void RapidFire(Transform tentacleTransform)
    {
        GameObject tempPro = Instantiate(projectile, tentacleTransform.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - tentacleTransform.position).normalized;
        tempPro.GetComponent<Rigidbody>().AddForce(direction * (velocity * 2), ForceMode.Force);

    }

    public void Shotgun()
    {
        for (int i = 0; i < 30; i++)
        {
            Quaternion tempRotation = Random.rotation;

            float minYOffset = Random.Range(-8, 0);
            float maxYOffset = Random.Range(8, 0);

            float minXoffset = Random.Range(-8, 0);
            float maxXoffset = Random.Range(8, 0);

            Vector3 projectileSpawn = new Vector3(((minXoffset + maxXoffset / 1.5f) + 0.5f), ((minYOffset + maxYOffset / 1.5f) + 0.5f), 0);

            GameObject rb = Instantiate(projectile, head.transform.position - gameObject.transform.right * projectileSpawn.x + gameObject.transform.up * projectileSpawn.y, Quaternion.identity);

            Vector3 finalDirection = (player.transform.position - head.transform.position).normalized;

            //then adds force to that projectile
            rb.GetComponent<Rigidbody>().AddForce(finalDirection * velocity, ForceMode.Impulse);
        }
        
    }
}
