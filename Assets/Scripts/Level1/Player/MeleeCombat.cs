using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private AudioSource fightSoundEffect;
    [SerializeField] private Transform hitController;
    [SerializeField] private float hitRadio;
    [SerializeField] private float hitDamage;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private float timeNextAttack;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeNextAttack > 0)
        {
            timeNextAttack -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire2") && timeNextAttack <= 0)
        {
            Hit();
            timeNextAttack = timeBetweenAttack;
        }
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
        fightSoundEffect.Play();
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitController.position, hitRadio);

        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.transform.GetComponent<Buziraco>().TakeDamage(hitDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitController.position, hitRadio);
    }
}
