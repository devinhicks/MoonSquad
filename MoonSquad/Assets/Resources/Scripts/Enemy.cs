using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    public int maxHealth = 5000;
    public int currentHealth;

    public Slider healthSlider;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthSlider.value = currentHealth;
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
    }

    public void Repair(int amount)
    {
        currentHealth += amount;
    }

    public void InitializeEnemy()
    {
        currentHealth = maxHealth;
    }
}
