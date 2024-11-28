using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Unity menu for setting the walk speed   
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    
 
    // Variable for the character controller
    private Rigidbody2D rb2d;
    // Variable for the player input handler
    private PlayerInputHandler inputHandler;
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
        rb2d = GetComponent<Rigidbody2D>();
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

        rb2d.transform.position += new Vector3(currentMovement.x, currentMovement.y, 0) * Time.deltaTime;
    }

    // Method to handle attacking
    void HandleAttacking() {
        if (inputHandler.AttackTriggered && weaponEquipped != null) {
            Debug.Log($"Attacking with weapon {weaponEquipped.name}");
            weaponEquipped.AttackWithEquipedWeapon(lookDirection);
        }
    }

    // Method to handle picking up things
    void HandlePickUp() {
        // If the player is near a weapon and the pick up button is pressed, pick up the weapon and set it as the weapon equipped and child to the player
 
        if (inputHandler.PickUpTriggered && weaponToPickUp != null) {
            if (weaponEquipped == null) {
                // Equip the new weapon
                EquipWeapon();
            } else {
                // Swap out the old weapon and drop it on the map
                weaponEquipped.transform.SetParent(null);
                weaponEquipped.GetComponent<Collider2D>().enabled = true;

                // Equip the new weapon
                EquipWeapon();
            }
            // Clear the reference to the weapon to pick up
            weaponToPickUp = null;
        } else if (inputHandler.PickUpTriggered && weaponEquipped == null) {
            Debug.Log("No weapon to pick up");
        }
    }

    void EquipWeapon() {
        if (weaponToPickUp != null) {
            weaponEquipped = weaponToPickUp.GetComponent<Weapon>();
            if (weaponEquipped != null) {
                weaponEquipped.transform.SetParent(transform);
                weaponEquipped.transform.localPosition = new Vector2(1, 0);
                weaponEquipped.transform.localRotation = Quaternion.identity;
                Collider2D weaponCollider = weaponEquipped.GetComponent<Collider2D>();
                if (weaponCollider != null) {
                    weaponCollider.enabled = false;
                }
            } else {
                Debug.LogError("Weapon component not found on weaponToPickUp");
            }
        }
    }

    // Method to handle scrolling
    void HandleScrolling() {
        // If the scroll value is not 0, log that the player is scrolling up or down
        if(inputHandler.ScrollValue != 0) {
        }
    }

    // Method to handle looking at the mouse position
    void HandleLooking() {
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
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Collided with " + collision.name);
        if(collision.CompareTag("Weapon")) {
            weaponToPickUp = collision.gameObject;
        }
    }
    // OnTriggerExit that are weapons and not colliding with player reset weaponToPickUp
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Weapon")) {
            weaponToPickUp = null;
        }
    }

}
