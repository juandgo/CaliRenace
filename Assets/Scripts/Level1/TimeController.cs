using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private Slider slider;
    private float actualTime;

    private bool activatedTime = false;

    // private void Start(){
    //     activateTimer();
    // }

    private void Update(){
        if( activatedTime ){
            ChangeCounter();
        }
    }

    private void ChangeCounter(){
        actualTime -= Time.deltaTime;

        if( actualTime >= 0 ){
            slider.value = actualTime;
        }

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
        slider.maxValue = maxTime;
        ChangeTimer(true);
    }

    public void deactivateTimer(){
        ChangeTimer(false);
    }
}
