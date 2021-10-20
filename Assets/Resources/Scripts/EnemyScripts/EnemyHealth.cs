using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyUIController enemyUIController;
    private SoundManagerScript soundManagerScript;

    private int enemyMaxHealth = 100;
    private int enemyCurrentHealth;
    
    void Start()
    {
        enemyUIController = GetComponent<EnemyUIController>();
        
        enemyCurrentHealth = enemyMaxHealth;
    }

    private void Update()
    {
        if (enemyCurrentHealth <= 0)
            DeathState();
        if (Input.GetKeyDown(KeyCode.O))
            SetSoundManager();   

    }
    public void ReduceHealth(int amount)
    {
        enemyCurrentHealth -= amount; //Reduce health

        //Sound
        if (soundManagerScript != null)
            soundManagerScript.PlaySound();
        print("Hitting");


        //Calculate percentage and change the visual health bar.
        float current = enemyCurrentHealth;
        float percentage = current / enemyMaxHealth;
        if(enemyUIController != null)
            enemyUIController.ChangeHealthBarValue(percentage);
    }
    private void DeathState() //You should know what this means
    {
        enemyUIController.DestroyHealthBar();
        Destroy(this.gameObject);
    }

    private void SetSoundManager()
    {
        soundManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManagerScript>();
    }
}
