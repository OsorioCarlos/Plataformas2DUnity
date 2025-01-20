using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    private int maxHealth = 10;
    private int currentHealth;
    private bool updateHealthInUI = false;
    private UISystem UISystem;

    public int Damage { get => damage; }
    public int Health { get => currentHealth; }

    void Awake()
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, health);
        UpdateUI();
    }

    public void Healing(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, health);
        UpdateUI();
    }

    public void ModifyHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        currentHealth = health;
        UpdateUI();
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void Defeat()
    {
        Destroy(gameObject);
    }

    public void EnableUIUpdate(UISystem UISystem)
    {
        updateHealthInUI = true;
        this.UISystem = UISystem;
        this.UISystem.InitializeHealthUI(currentHealth);
    }

    private void UpdateUI()
    {
        if (updateHealthInUI == true)
        {
            UISystem.UpdateHealthUI(currentHealth);
        }
    }
}
