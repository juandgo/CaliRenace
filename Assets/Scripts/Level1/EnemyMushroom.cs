using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : MonoBehaviour
{
    [SerializeField] private GameObject efecto;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetContact(0).normal.y <= -0.9)
            {
                animator.SetTrigger("Hit");
                other.gameObject.GetComponent<MainPlayer>().Bounce();
            }
        }
    }

    private void Hit(){
        Instantiate(efecto, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
