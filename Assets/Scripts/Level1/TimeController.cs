using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float maxTime;
    private float actualTime;

    private bool activatedTime = false;

    private void Update(){
        if( activatedTime ){
            ChangeCounter();
        }
    }

    private void ChangeCounter(){
        actualTime -= Time.deltaTime;

        if(actualTime <= 0){
            Debug.Log("Derrota");
            ChangeTimer(false);
        }
    }

    private void ChangeTimer(bool status){
        activatedTime = status;
    }

    public void activateTimer(){
        actualTime = maxTime;
        ChangeTimer(true);
    }

    public void deactivateTimer(){
        ChangeTimer(false);
    }
}
