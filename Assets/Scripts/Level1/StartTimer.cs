using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private TimeController timeController;

    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.CompareTag("Player")){
            timeController.activateTimer();
        }
    }
    
}
