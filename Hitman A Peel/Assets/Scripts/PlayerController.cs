using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
    // Variable for interacting with hideout
    private Hideout currentHideout;
    // Variable for the weapon equipped
    private GameObject weaponEquipped;
    // Variable for the camera transform
    private Transform cameraTransform;
    // Variable for the look direction
    private Vector3 lookDirection;


    // Awake method to get the character controller and input handler and camera transform
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        inputHandler = PlayerInputHandler.Instance;
        cameraTransform = Camera.main.transform.parent;
    }

    // Update method to handle movement, attacking, picking up, and scrolling
    private void Update() {
        HandleMovement();
        HandleAttacking();
        HandlePickUp();
        HandleInteraction();
        HandleScrolling();
        HandleLooking();
        UpdateCameraPosition();
    }

    // Method to handle movement
    void HandleMovement() {
        float speed = walkSpeed;

        // moveDirection is the input from the player
        Vector2 moveDirection = new Vector2(inputHandler.MoveInput.x, inputHandler.MoveInput.y);
        currentMovement = moveDirection.normalized * speed;

        characterController.Move(currentMovement * Time.deltaTime);
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

    void HandleInteraction()
    {
        // If the pick up button is pressed, log that the player is picking up
        if(inputHandler.PickUpTriggered) {
            if (weaponToPickUp != null) {
                HandlePickUp();
            }
            else if (currentHideout != null) {
                HidePlayer();
            }
        }            
    }

    void HidePlayer() {
        Debug.Log("Player is hiding:))");
        characterController.enabled = false;
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
        Debug.Log($"Looking at {inputHandler.LookInput}");
        Vector2 mousePosition = inputHandler.LookInput;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        lookDirection = mouseWorldPosition - transform.position;
        lookDirection.z = 0;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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
        else if (other.CompareTag("Hideout")) {
            currentHideout = other.GetComponent<Hideout>();
        }
    }
    // OnTriggerExit that are weapons and not colliding with player reset weaponToPickUp
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Weapon")) {
            weaponToPickUp = null;
        }
        else if (other.CompareTag("Hideout")) {
            currentHideout = null;
        }
    }

}
