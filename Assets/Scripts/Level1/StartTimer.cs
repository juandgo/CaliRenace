using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private TimeController timeController;
    // [SerializeField] public AudioSource timerSound;
    // [SerializeField] private Level1 lv1;

    private void OnTriggerEnter2D(Collider2D other){
        // if(other.CompareTag("Player")){
        //     timeController.activateTimer();
        //     timerSound.Play();
        // }

        if(other.gameObject.CompareTag("Player")){
            timeController.activateTimer();
            //  lv1.fxSource.Stop();
            // timerSound.Play();
        }
    }
    
}
