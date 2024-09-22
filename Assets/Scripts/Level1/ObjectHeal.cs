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
            // Destroy(collider.gameObject);
            playerMovement.HealLife(heal);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Heart"))
    //     {
    //         // collectionSoundEffect.Play();
    //         Destroy(collision.gameObject);
    //         playerMovement.HealLife(heal);

    //     }
    // }
}
