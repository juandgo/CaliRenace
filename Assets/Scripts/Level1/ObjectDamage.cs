using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public int damage;
    [SerializeField] private AudioSource collectDamageSoundEffect;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar y aplicar daño al jugador si tiene el componente PlayerLife
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            collectDamageSoundEffect.Play();
            playerMovement.TakeDamage(damage);
        }
    }
}
