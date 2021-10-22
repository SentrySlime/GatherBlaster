using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private EnemyAI enemyAI;

    
    private bool move = true;
    private Vector3 targetPosition;

    //--ROAM--//
    private Vector3 startingPosition; //The enemy origin from where it will roam
    private Vector3 walkPoint; //The position it should walk to
    


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();

        //Set the speed of the agent
        agent.speed = enemyAI.moveSpeed;

        //Initialize roaming point
        walkPoint = GetRoamingPoint();
        startingPosition = transform.position;
    }

    void Update()
    {
        if (move)
            //Just walk mate
            agent.SetDestination(targetPosition);
    }


    public void SetTargetPos(Vector3 target) //Call this whenever the enemy should change direction
    {
        targetPosition = target;
    }
    public void ControlMovement(bool allowMovement)
    {
        move = allowMovement;
    }


    //--ROAM--//
    private Vector3 GetRoamingPoint()
    {
        //Startingposition plus random direction multiplied by a random distance.
        return startingPosition + Utils.GetRandomDirNoY() * Random.Range(10f, enemyAI.roamRadius);

    }
    public void Roam()
    {
        //Check if the body has reached the target roaming point
        if (Vector3.Distance(transform.position, walkPoint) < 0.1f)
        {
            //The body has reached target.
            //Brain needs to create new target
            walkPoint = GetRoamingPoint();
            SetTargetPos(walkPoint);
        }
        else
        {
            SetTargetPos(walkPoint);
        }
    }
    
}
