using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fjende : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    private float health;
    SpriteRenderer spriteRenderer;
    public HealthBar healthBar;


    [SerializeField]
    Sprite DeadBanana;
    [SerializeField]
    bool IsDead = false;
    Animator animator;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(100);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            IsDead = true;
        }
        if (IsDead)
        {
            animator.enabled = false;
            spriteRenderer.sprite = DeadBanana;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }
}
