using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour //This is the brains of the operation.
{
    private enum EnemyState
    {
        Roaming,
        ChaseTarget,
        Attacking
    }
    
    public float roamRadius = 20f;
    public float detectionRadius = 20f;
    public float attackRange = 10f;
    
    
    private EnemyState currentEnemyState;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;

    private Vector3 startingPosition; //The enemy origin from where it will roam
    private Vector3 walkPoint; //The position it should walk to

    private GameObject player;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentEnemyState = EnemyState.Roaming;
    }
    void Start()
    {
        startingPosition = transform.position;
        walkPoint = GetRoamingPoint();
    }

    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Roaming:

                FindTarget(); //Check if any target is near, for now that's the player.

                //The brains tell the body where to go!
                enemyMovement.SetTargetPos(walkPoint);

                Roam();
                break;
            case EnemyState.ChaseTarget:
                enemyMovement.SetTargetPos(player.transform.position); //Make body move towards the player.

                if (Vector3.Distance(transform.position, player.transform.position) < attackRange) //Check if target is within range
                {
                    //Stopping movement
                    enemyMovement.ControlMovement(false);

                    //Shoot
                    if (enemyAttack.currentAttackPrefab == null) //Check if enemy already has been lent a attackpattern by bulletspawner
                        enemyAttack.RequestNewAttackPrefab(player); //If not request it so we can attack.
                    else
                    {
                        if (enemyAttack.currentPatternController.typeOfPattern != enemyAttack.currentAttackPattern)
                        {
                            //If we have a pattern check that it's the one we wanna use.
                            enemyAttack.ReturnCurrentAttackPrefab();
                            enemyAttack.RequestNewAttackPrefab(player);
                        }
                        else
                            enemyAttack.Shoot(true); //We already have a pattern to use.
                    }
                }
                else
                {
                    enemyAttack.Shoot(false); //Stop shooting
                    enemyMovement.ControlMovement(true);
                }
                        
                break;
        }
            
    }
    private Vector3 GetRoamingPoint()
    {
        //Startingposition plus random direction multiplied by a random distance.
        return startingPosition + Utils.GetRandomDirNoY() * Random.Range(10f, roamRadius); 

    }
    private void Roam()
    {
        //Check if the body has reached the target roaming point
        if (Vector3.Distance(transform.position, walkPoint) < 0.1f)
        {
            //The body has reached target.
            //Brain needs to create new target
            walkPoint = GetRoamingPoint();
        }
    }
    private void FindTarget()
    {
        //Check if player is within range
        if(Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
        {
            currentEnemyState = EnemyState.ChaseTarget;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, roamRadius);
    }


}
