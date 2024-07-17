using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public int life;
    public event EventHandler deathPlayer;

    public void Damage(int amountDamage){
        life -= amountDamage;
        if(life <= 0){
            deathPlayer?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject); 
        }
    }

}
