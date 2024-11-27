using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public HealthBar healthBar; // health bar for the player
    public float moveSpeed = 5f;
    public Rigidbody2D rb; 
    public weapon weapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Start()
    {
        healthBar.SetMaxHealth(100); // set the max health of the player to 100
    }
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); 
        float moveY = Input.GetAxisRaw("Vertical");
    if(Input.GetMouseButtonDown(0))
    {
        weapon.Fire();
    }
        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}


