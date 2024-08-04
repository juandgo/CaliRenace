using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTimer : MonoBehaviour
{
    [SerializeField] private TimeController timeController;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            timeController.deactivateTimer();
        }
    }
}
