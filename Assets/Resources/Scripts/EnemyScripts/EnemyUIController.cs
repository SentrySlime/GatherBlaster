using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] GameObject enemyUICanvas;
    [SerializeField] GameObject enemyHealthBarPrefab;

    public Vector3 healthBarPos; // Adjust this to move the health bar where you want it

    public float hideEnemyUIDistance = 75f;

    private GameObject healthBar;
    private Image healthBarFill;
    
    void Start()
    {
        //Instantiates the enemy health bar as a child of the UI Canvas. We then store the image that will fill the health bar as a variable.
        healthBar = Instantiate(enemyHealthBarPrefab, enemyUICanvas.transform);
        healthBarFill = healthBar.transform.Find("EnemyHealth_Fill").GetComponent<Image>();
    }

    
    void Update()
    {
        PositionHealthBar();
    }
    public void ChangeHealthBarValue(float fillAmountPercentage) //This value needs to be between 1 and 0. Hence, "percentage".
    {
        healthBarFill.fillAmount = fillAmountPercentage;
    }
    private void PositionHealthBar() //Since the health bar isn't a child of the enemy gameobject we need to manually move it.
    {
        //Check if player is close enough to show healthbar. Otherwise, hide it.
        if (Vector3.Distance(transform.position, ThirdPersonMovement.Instance.GetPlayerPosition()) < hideEnemyUIDistance)
        {
            HideBar(healthBar, false); //Show
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarPos);
        }
        else
        {
            HideBar(healthBar, true); //Hide
        }
            

            

    }

    public void DestroyHealthBar()
    {
        Destroy(healthBar);
    }
    private void HideBar(GameObject bar, bool hide)
    {
        if (hide)
            bar.SetActive(false);
        else if (!hide)
            bar.SetActive(true);
    }

}
