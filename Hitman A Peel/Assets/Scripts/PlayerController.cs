using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Unity menu for setting the walk speed   
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    
 
    // Variable for the character controller
    private CharacterController characterController;
    // Variable for the player input handler
    private PlayerInputHandler inputHandler;
    // Variable for the weapon
    private Weapon weapon;
    // Variable for the current movement
    private Vector2 currentMovement;
    // Variable for the weapon to pick up
    private GameObject weaponToPickUp;
    // Variable for the weapon equipped
    public Weapon weaponEquipped;
    // Variable for the camera transform
    private Transform cameraTransform;
    // Variable for the look direction
    public Vector3 lookDirection;

    // Awake method to get the character controller and input handler and camera transform
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        inputHandler = PlayerInputHandler.Instance;
        cameraTransform = Camera.main.transform.parent;

    }

    // Update method to handle movement, attacking, picking up, and scrolling
    private void Update() {
        if (inputHandler == null) {
            Debug.LogError("PlayeInputHandler instance is not set");
            return;
        }

        HandleMovement();
        HandleAttacking();
        HandlePickUp();
        HandleScrolling();
        HandleLooking();
        UpdateCameraPosition();
    }

    // Method to handle movement
    void HandleMovement() {
        float speed = walkSpeed;

        // moveDirection is the input from the player
        Vector2 inputDirection = new Vector2(inputHandler.MoveInput.x, inputHandler.MoveInput.y);
        Vector3 moveDirection = new Vector3(inputDirection.x, inputDirection.y);

        currentMovement = moveDirection.normalized * speed;

        characterController.Move(currentMovement * Time.deltaTime);
    }

    // Method to handle attacking
    void HandleAttacking() {
        if(inputHandler.AttackTriggered) {
            Debug.Log("Attacking");
        }

        if (inputHandler.AttackTriggered && weaponEquipped != null) {
            weaponEquipped.AttackWithEquipedWeapon();
        } else if (weaponEquipped == null) {
            Debug.Log("No Weapon component equipped");
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
            if (weaponEquipped != null) {
                weaponEquipped.transform.SetParent(null);
                weaponEquipped.GetComponent<Collider>().enabled = true;
                weaponEquipped.transform.position = weaponToPickUp.transform.position;


            }
            weaponEquipped = weaponToPickUp.GetComponent<Weapon>();
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
        Debug.Log($"Looking at {inputHandler.LookInput}");
        Vector2 mousePosition = inputHandler.LookInput;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        lookDirection = mouseWorldPosition - transform.position;
        lookDirection.z = 0;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (weaponEquipped != null) {
            weaponEquipped.SetLookDirection(lookDirection);
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
