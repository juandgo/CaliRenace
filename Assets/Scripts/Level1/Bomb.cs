using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float radio;
    [SerializeField] private float explosiveForce;


    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Explode();
        }
    }

    public void Explode(){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);

        foreach (Collider2D colisionador in objetos){
            Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null){
                Vector2 direction = colisionador.transform.position - transform.position;
                float distance = 1 + direction.magnitude;
                float finalForce = explosiveForce /distance;
                rb2D.AddForce(direction * finalForce);
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
