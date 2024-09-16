using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public int damage;

    // private void OnTriggerEnter2D(Collider2D collider){
    //     if (collider.TryGetComponent(out PlayerLife playerLife)){
    //         playerLife.Damage(damage);
    //     }
    // }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Verificar y aplicar daño al jugador si tiene el componente PlayerLife
    //     if (collision.gameObject.TryGetComponent(out PlayerLife playerLife))
    //     {
    //         playerLife.Damage(damage);
    //     }
    // }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar y aplicar daño al jugador si tiene el componente PlayerLife
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.Damage(damage);
        }
    }
}
