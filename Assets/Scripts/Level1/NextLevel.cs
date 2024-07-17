using System.Collections;
using System.Collections.Generic;
using LevelUnlockSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // [SerializeField] private GameObject lockObj, unlockObj;     //ref to lock and unlock gameobject 
    private LevelSystemManager levelSystemManager;
    public string nextLevel;
    [SerializeField] private AudioSource sound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collided object has the tag "Player"
        if (collider.CompareTag("Player"))
        {
            // Start the coroutine to wait for the sound to finish
            StartCoroutine(WaitForSoundAndLoadLevel());
        }
    }

    private IEnumerator WaitForSoundAndLoadLevel()
    {
        // Play the sound
        sound.Play();

        // Wait until the sound has finished playing
        while (sound.isPlaying)
        {
            yield return null;
        }

        // Load the next level
        SceneManager.LoadScene(nextLevel);
        // lockObj.SetActive(false);                           //deactivate lockObj
        // unlockObj.SetActive(true);  
        // levelSystemManager.LevelComplete(1);
    }
}
