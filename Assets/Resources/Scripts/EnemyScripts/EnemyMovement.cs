using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float speed = 5f;
    private bool move = true;
    private Vector3 targetPosition;


    void Start()
    {
    }

    void Update()
    {
        if(move)
            //Just walk mate
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    public void SetTargetPos(Vector3 target) //Call this whenever the enemy should change direction
    {
        targetPosition = target;
    }
    public void ControlMovement(bool allowMovement)
    {
        move = allowMovement;
    }

}
