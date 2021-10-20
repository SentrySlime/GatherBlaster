using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    private int playerMaxHealth = 100;
    private int playerCurrentHealth;

    private Image playerHealthBar;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        playerHealthBar = GameObject.Find("PlayerHealth_Fill").GetComponent<Image>();
    }

    
    void Update()
    {
        
    }
    public void ReduceHealth(int amount)
    {
        playerCurrentHealth -= amount;
        Debug.Log("Player health: " + playerCurrentHealth);

        //Update visuals
        float current = playerCurrentHealth;
        float percentage = current / playerMaxHealth;
        playerHealthBar.fillAmount = percentage;
    }
}
