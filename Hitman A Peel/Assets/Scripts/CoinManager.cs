using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour // Defining the CoinManager class that inherits from MonoBehaviour
{
    public int coinCount; // Public integer to store the count of coins
    public Text coinText; // Public Text object to display the coin count
    public GameObject door; // Public GameObject to reference the door
    private bool doorDestroyed; // Private boolean to check if the door is destroyed

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code can be placed here
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin Count: " + coinCount.ToString(); // Update the coinText with the current coin count
        if (coinCount == 6 && !doorDestroyed) // Check if coinCount is 6 and the door is not yet destroyed
        {
            doorDestroyed = true; // Set doorDestroyed to true
            Destroy(door); // Destroy the door GameObject
        }
    }
}
