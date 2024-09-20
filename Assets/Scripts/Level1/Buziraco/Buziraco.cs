using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buziraco : MonoBehaviour
{
    public Transform player; // Se añade la referencia al jugador
    public float detectionRadius = 5.0f; // Corregido el nombre
    public float speed = 2.0f;
    private Rigidbody2D rb; // Cambiado a Rigidbody2D
    private Vector2 movement;
    private Animator animator;

    private bool onTheMove;

    [SerializeField] private float life;
    [SerializeField] private GameObject killingEfect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener Rigidbody2D
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRadius)
            {
                // Normalizar la dirección del movimiento
                Vector2 direction = (player.position - transform.position).normalized;
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                movement = new Vector2(direction.x, 0); // Usar tanto X como Y si es necesario
                onTheMove = true;
            }
            else
            {
                movement = Vector2.zero; // Corregido el uso de Vector2.zero
                onTheMove = false;
            }
        }


        rb.MovePosition(rb.position + movement * speed * Time.deltaTime); // Mover con Rigidbody2D
        animator.SetBool("onTheMove", onTheMove);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(killingEfect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
