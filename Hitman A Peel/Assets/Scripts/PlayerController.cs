using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // Unity menu for setting the walk speed   
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
 
    // Variable for the character controller
    private CharacterController characterController;
    // Variable for the player input handler
    private PlayerInputHandler inputHandler;
    // Variable for the current movement
    private Vector2 currentMovement;
    // Variable for the weapon to pick up
    private GameObject weaponToPickUp;
    // Variable for the weapon equipped
    private GameObject weaponEquipped;
    // Variable for the camera transform
    private Transform cameraTransform;
    // Variable for the look direction
    private Vector3 lookDirection;

    [SerializeField]
    GameObject pointer;

    Animator animator;

    // Awake method to get the character controller and input handler and camera transform
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        inputHandler = PlayerInputHandler.Instance;
        cameraTransform = Camera.main.transform.parent;
        animator = GetComponent<Animator>();
    }

    // Update method to handle movement, attacking, picking up, and scrolling
    private void Update() {
        HandleMovement();
        HandleAttacking();
        HandlePickUp();
        HandleScrolling();
        HandleLooking();
        UpdateCameraPosition();
    }

    // Method to handle movement
    void HandleMovement() {
        float xMovement = inputHandler.MoveInput.x;
        float yMovement = inputHandler.MoveInput.y;

        // moveDirection is the input from the player
        Vector2 moveDirection = new Vector2(xMovement, yMovement);
        currentMovement = moveDirection.normalized * walkSpeed;
        characterController.Move(currentMovement * Time.deltaTime);

        //The animation playing will be determined by xMovement and yMovement (Floats in range 0,1), with S_Walk, D_Walk and A_Walk being prioritized on angular walking
        if (xMovement != 0 || yMovement != 0)
        {
            animator.SetBool("isWalking", true);
            if (yMovement == 1 && xMovement == 0) // A_Walk
            {
                animator.SetFloat("y", 1f);
                animator.SetFloat("x", 0);
            }

        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("x", 0);
            animator.SetFloat("y", -1);
        }
    }

    // Method to handle attacking
    void HandleAttacking() {
        if(inputHandler.AttackTriggered) {
            Debug.Log("Attacking");
        }

        // If the attack button is pressed and the player has a weapon equipped, attack in the direction the player is looking
        if(inputHandler.AttackTriggered && weaponEquipped != null) {
            Debug.Log("Player is Attacking");
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = lookDirection.normalized * projectileSpeed;
        }
    }

    // Method to handle picking up things
    void HandlePickUp() {
        // If the pick up button is pressed, log that the player is picking up
        if(inputHandler.PickUpTriggered) {
            Debug.Log("Picking Up");
        }

        // If the player is near a weapon and the pick up button is pressed, pick up the weapon and set it as the weapon equipped and child to the player
        if(inputHandler.PickUpTriggered && weaponToPickUp != null) {
            Debug.Log("Picking Up Weapon");
            weaponToPickUp.transform.SetParent(transform);
            weaponToPickUp.transform.localPosition = new Vector2(1, 0);
            Collider weaponCollider = weaponToPickUp.GetComponent<Collider>();
            if(weaponCollider != null) {
                weaponCollider.enabled = false;
            }
            weaponEquipped = weaponToPickUp;
            weaponToPickUp = null;
        }

        
    }

    // Method to handle scrolling
    void HandleScrolling() {
        // If the scroll value is not 0, log that the player is scrolling up or down
        if(inputHandler.ScrollValue != 0) {
            if (inputHandler.ScrollValue > 0) {
                Debug.Log("Scrolling Down");
            } else {
                Debug.Log("Scrolling Up");
            }
        }
    }

    // Method to handle looking at the mouse position
    void HandleLooking() {
        //Debug.Log($"Looking at {inputHandler.LookInput}");
        //Debug.Log($"Player at {transform.position}");
        Vector2 mousePosition = inputHandler.LookInput;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        lookDirection = mouseWorldPosition - transform.position;
        lookDirection.z = 0;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.Euler(0, 0, angle);
        
        if(angle >= -45 && angle < 45) //D direction
        {
            animator.SetFloat("x", 1);
            animator.SetFloat("y", 0);
        } else if (angle >= 45f && angle < 135f) // W direction
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 1);
        } else if (angle >= 135f || angle < -180) // A direction
        {
            animator.SetFloat ("x", -1);
            animator.SetFloat("y", 0);
        } else if (angle < -45 && angle >= -135) // S Direction
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", -1);
        }
    }

    // Method to update the camera position to follow the player
    void UpdateCameraPosition() {
        if (cameraTransform != null) {
            cameraTransform.position = new Vector3(transform.position.x, transform.position.y, cameraTransform.position.z);
        }
    }

    // OnTriggerEnter that are weapons and colliding with player to pick up 
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Weapon")) {
            weaponToPickUp = other.gameObject;
        }
    }
    // OnTriggerExit that are weapons and not colliding with player reset weaponToPickUp
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Weapon")) {
            weaponToPickUp = null;
        }
    }

}
