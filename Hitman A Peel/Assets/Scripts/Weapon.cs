using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    // Variable for the weapon's damage
    //[SerializeField] private float damage = 10.0f;
    // Variable for the weapon's range
    [SerializeField] private float range = 10.0f;
    // Variable for the weapon's fire rate
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10.0f;

    // Variable for the player input handler
    private PlayerInputHandler inputHandler;
    // Variable for the weapon's next time to fire
    private float nextTimeToFire = 0.0f;
    private Vector3 lookDirection;
    private void Awake() {
        inputHandler = PlayerInputHandler.Instance;
    }

    // Method to handle attacking
    public void AttackWithEquipedWeapon() {
        if(Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1.0f / fireRate;

            Debug.Log("Player is Attacking");
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            if (projectileRigidbody != null) {
                projectileRigidbody.velocity = lookDirection.normalized * projectileSpeed;
            }
            
            StartCoroutine(DestroyProjectileAfterRange(projectile));
        }
    }

    // Method to destroy the projectile after it reaches the range
    private IEnumerator DestroyProjectileAfterRange(GameObject projectile) {
        float rangeTravel = range / projectileSpeed;
        yield return new WaitForSeconds(rangeTravel);
        if (projectile != null) {
            Destroy(projectile);
        }
    }

    public void SetLookDirection(Vector3 lookDirection) {
        this.lookDirection = lookDirection;
    }
}
