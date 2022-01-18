using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //Script references
    //public PlayerMoveScript playerMoveScript;

    public GameObject player;
    public GameObject playerModel;
    public bool modelBool;


    public GameObject projectile;
    public GameObject barrel;
    public Camera cameraObject;
    public float projectileSpeed;

    [Header("Rifle Stats")]
    public float fireRate;
    float timer;

    [Header("Shotgun Stats")]
    public float fireRate2;
    public float spreadAngle;
    public float shotgunOffset;
    public int pelletAmount;

    public float sphereCastRadius;
    public LayerMask layermask;

    public float currentHitDistance;
    public float maxDistance;

    Vector3 permVec;

    public enum Weapon
    {
        fullAuto,
        shotgun
    }

    public Weapon currentWeapon;

    void Start()
    {
        currentHitDistance = maxDistance;

        //#if UNITY_EDITOR
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;
        //#endif
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    MeshRenderer lastMesh;
    
    void Update()
    {
        
        //permVec = ( - transform.position).normalized;
        //Vector3 tempVec = (player.transform.position - transform.position).normalized;
        float tempDis = Vector3.Distance(player.transform.position, transform.position);

        //Ray ray2 = new Ray(transform.position, transform.forward);
        //RaycastHit hit2;
        //if (Physics.SphereCast(transform.position, 6, permVec, out hit2, tempDis, layermask))
        //{
        //    Debug.DrawRay(transform.position, permVec * 5000, Color.red);
        //    //print("Grounded");

        //    MeshRenderer meshRend = hit2.collider.gameObject.GetComponent<MeshRenderer>();
        //    lastMesh = meshRend;
        //    meshRend.enabled = false;

        //    //GameObject tempObj = hit2.collider.gameObject;
        //    //tempObj.SetActive(false);



        //}
        //else if(lastMesh != null)
        //{
        //    lastMesh.enabled = true;
        //    lastMesh = null;
        //}

        HidePlayer();

        //Firerate timer goes up between shots
        if (currentWeapon == Weapon.fullAuto)
        {
            if (timer < fireRate)
                timer += Time.deltaTime;
        }
        else if(currentWeapon == Weapon.shotgun)
        {
            if (timer < fireRate2)
                timer += Time.deltaTime;
        }
        

        if (Input.GetKeyDown(KeyCode.X))
            SwitchWeapon();

        //If you press button and the timer is above firerate 
        if(Input.GetMouseButton(0))
        {
            

            //If you have the full auto weapon
            if(currentWeapon == Weapon.fullAuto && timer >= fireRate)
            {
                //Reset firerate timer
                timer = 0;
                //Create the bullet
                GameObject rb = Instantiate(projectile, barrel.transform.position, transform.rotation);
                
                //Create raycasts
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                //If the raycast hits an enemy or enviroment
                //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layermask))
                if (Physics.SphereCast(transform.position, sphereCastRadius, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layermask))
                {
                    currentHitDistance = hit.distance;

                    //You set the direction to where raycast hit
                    Vector3 finalDirection = (hit.point - barrel.transform.position).normalized;

                    //Add force in that direction
                    rb.GetComponent<Rigidbody>().AddForce(finalDirection * projectileSpeed, ForceMode.Impulse);
                    
                    //Debug line
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 2);

                    
                }
                else
                {
                    //If raycast hit nothing you shoot the bullet stright forward
                    rb.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);

                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red, 2);

                    currentHitDistance = maxDistance;
                }

            }//If you have the shotgun weapon
            else if(currentWeapon == Weapon.shotgun && timer >= fireRate2)
            {
                //Reset firerate timer
                timer = 0;
                //For every projectile your shotgun shoots
                for (int i = 0; i < pelletAmount; i++)
                {
                    Ray ray = new Ray(transform.position, transform.forward);
                    RaycastHit hit;

                    if (Physics.SphereCast(transform.position, sphereCastRadius, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layermask))
                    {
                        currentHitDistance = hit.distance;
                        //You create a random direction
                        Quaternion tempRotation = Random.rotation;

                        float minYOffset = Random.Range(-shotgunOffset, 0);
                        float maxYOffset = Random.Range(shotgunOffset, 0);

                        float minXoffset = Random.Range(-shotgunOffset, 0);
                        float maxXoffset = Random.Range(shotgunOffset, 0);

                        Vector3 projectileSpawn = new Vector3(((minXoffset + maxXoffset / 1.5f) + 0.5f), ((minYOffset + maxYOffset / 1.5f) + 0.5f), 0);

                        GameObject rb = Instantiate(projectile, barrel.transform.position - cameraObject.transform.right * projectileSpawn.x + cameraObject.transform.up * projectileSpawn.y, Quaternion.identity);
                        
                        Vector3 finalDirection = (hit.point - barrel.transform.position).normalized;

                        //then adds force to that projectile
                        rb.GetComponent<Rigidbody>().AddForce(finalDirection * projectileSpeed, ForceMode.Impulse);

                    }
                    else
                    {

                        currentHitDistance = maxDistance;
                        //You create a random direction
                        Quaternion tempRotation = Random.rotation;

                        float minYOffset = Random.Range(-shotgunOffset, 0);
                        float maxYOffset = Random.Range(shotgunOffset, 0);

                        float minXoffset = Random.Range(-shotgunOffset, 0);
                        float maxXoffset = Random.Range(shotgunOffset, 0);

                        Vector3 projectileSpawn = new Vector3(((minXoffset + maxXoffset / 1.5f) + 0.5f), ((minYOffset + maxYOffset / 1.5f) + 0.5f), 0);

                        GameObject rb = Instantiate(projectile, barrel.transform.position - cameraObject.transform.right * projectileSpawn.x + cameraObject.transform.up * projectileSpawn.y, Quaternion.identity);

                        Vector3 finalDirection = (transform.forward - barrel.transform.position).normalized;

                        //then adds force to that projectile
                        rb.GetComponent<Rigidbody>().AddForce(cameraObject.transform.forward * projectileSpeed, ForceMode.Impulse);

                    }
                }
                
            }

        }
    }
    
    public void SwitchWeapon()
    {
        if (currentWeapon == Weapon.fullAuto)
            currentWeapon = Weapon.shotgun;
        else if (currentWeapon == Weapon.shotgun)
            currentWeapon = Weapon.fullAuto;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Debug.DrawLine(transform.position, permVec + permVec * currentHitDistance);
        Gizmos.DrawWireSphere(player.transform.forward * currentHitDistance, sphereCastRadius);
    }

    private void HidePlayer()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);


        if(distance < 3.5 && modelBool == false)
        {
            playerModel.SetActive(false);
            modelBool = true;
        }
        else if(distance > 3.5 && modelBool == true)
        {
            playerModel.SetActive(true);
            modelBool = false;
        }
    }



}
