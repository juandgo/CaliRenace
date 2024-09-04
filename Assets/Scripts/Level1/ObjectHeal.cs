using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHeal : MonoBehaviour
{
    public int heal;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.HealLife(heal);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar y aplicar daño al jugador si tiene el componente PlayerLife
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.HealLife(heal);
        }
    }
}
