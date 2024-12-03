using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    private void OnDestroy()
    {
        // Restart the scene when the Player object is destroyed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
