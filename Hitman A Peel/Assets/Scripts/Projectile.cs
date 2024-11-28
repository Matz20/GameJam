using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            Debug.Log("Projectile hit enemy");
            Destroy(gameObject);
        }
    }
}
