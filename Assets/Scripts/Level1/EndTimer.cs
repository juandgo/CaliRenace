using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
    [SerializeField] private StartTimer sTimer;
    [SerializeField] private TimeController timeController;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            timeController.deactivateTimer();
            sTimer.timerSound.Stop(); 
        }
    }
}
