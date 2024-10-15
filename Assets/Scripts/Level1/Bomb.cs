using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float radio;
    [SerializeField] private float explosiveForce;
    [SerializeField] private GameObject explosiveEfect;


    private void Update(){
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Explode();
        // }
        if(Input.GetButtonDown("Fire3")){
            Explode();
        }
    }

    public void Explode(){
        Instantiate(explosiveEfect, transform.position, Quaternion.identity);
        // CinemachineMovimientoCamara.Instance.MoverCamara(10,10,1);
        CinemachineCameraMovement.Instance.MoveCamera(10, 10, 1);
        // Collider2D[] objetosIniciales = Physics2D.OverlapCircleAll(transform.position, radio);

        // foreach(Collider2D colisionador in objetosIniciales){
        //     Box box = colisionador.GetComponent<Box>();
        //     if(box != null){
        //         box.Destroy();
        //     }
        // }

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
