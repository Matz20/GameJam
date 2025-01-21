using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChecker : MonoBehaviour // Defining the CoinChecker class that inherits from MonoBehaviour
{
    public CoinManager cm; // Public reference to the CoinManager script

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be placed here
    }

    // Update is called once per frame
    void Update()
    {
        // Code to be executed every frame can be placed here
    }

    private void OnTriggerEnter2D(Collider2D other) // Method called when another collider enters the trigger collider attached to the object where this script is attached
    {
        if (other.gameObject.tag == "Coin") // Check if the collided object has the tag "Coin"
        {
            Destroy(other.gameObject); // Destroy the coin GameObject
            cm.coinCount++; // Increment the coin count in the CoinManager script
        }
    }
}