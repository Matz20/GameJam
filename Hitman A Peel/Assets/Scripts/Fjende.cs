using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fjende : MonoBehaviour
{
[SerializeField] float maxHealth = 100f;
private float health;

public HealthBar healthBar;
    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(100);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
  
}
