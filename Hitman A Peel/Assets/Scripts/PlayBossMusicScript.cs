using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger!");

            // Stop all other AudioSources
            StopOtherAudioSources();

            // Play this trigger's sound
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log("Playing sound from this trigger.");
            }
        }
    }

    private void StopOtherAudioSources()
    {
        // Find all AudioSource components in the scene
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allAudioSources)
        {
            // Stop the source if it is playing and is not this trigger's source
            if (source != audioSource && source.isPlaying)
            {
                source.Stop();
                Debug.Log("Stopped audio from: " + source.gameObject.name);
            }
        }
    }
}
