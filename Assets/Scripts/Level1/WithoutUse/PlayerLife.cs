using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerLife : MonoBehaviour
{
    
    public int actualLife, maxLife, valorPrueba;

    public UnityEvent<int> changeLife;

    public event EventHandler deathPlayer;

    private void Start()
    {
        actualLife = maxLife;
        changeLife.Invoke(actualLife);
    }
    
    private void Update(){

        //This code works with click rigth and click left, It gives life and takes life 
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     Damage(valorPrueba);
        // }

        // if (Input.GetButtonDown("Fire2"))
        // {
        //     HealLife(valorPrueba);
        // }
    }

    public void Damage(int amountDamage){
        int temporalLife = actualLife - amountDamage;

        if(temporalLife < 0){
            actualLife = 0;
        }else{
            actualLife = temporalLife;
        }

        changeLife.Invoke(actualLife);

        if(actualLife <= 0){
            deathPlayer?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }

    }

    public void HealLife(int amountHeal){
        int temporalLife = actualLife + amountHeal;

        if(temporalLife > maxLife){
            actualLife = maxLife;
        }else{
             actualLife = temporalLife;
        }
        changeLife.Invoke(actualLife);
    }
}
