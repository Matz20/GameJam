using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideout : MonoBehaviour
{
    public GameObject player;
    private bool hiding;
    private bool nearHideout;

    void Update()
    {
        if (nearHideout && Input.GetKeyDown(KeyCode.F))
        {
            HidePlayer();
            hiding = true;
            Debug.Log(hiding);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearHideout = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearHideout = false;
        }
    }

    private void HidePlayer()
    {
        var playerMovement = player.GetComponent<PlayerController>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }
}
