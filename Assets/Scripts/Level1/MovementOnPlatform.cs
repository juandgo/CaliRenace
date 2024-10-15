using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementOnPlatform : MonoBehaviour
{
    [SerializeField] private float speed, groundDistance, wallDistance;
    [SerializeField] private Transform groundController;
    [SerializeField] private Transform wallController;
    public bool wallInRange;
      public LayerMask layerGround;
    [SerializeField] private bool moveRight;

    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        
        // Inicializamos para que el personaje se mueva hacia la izquierda
        if (!moveRight) {
            speed = -Mathf.Abs(speed); // Asegura que la velocidad sea negativa
            transform.eulerAngles = new Vector3(0, 180, 0); // Gira el personaje para mirar hacia la izquierda
        }
    }

    private void Update(){
        wallInRange = Physics2D.Raycast(wallController.position, transform.right, wallDistance, layerGround);

        if (wallInRange)
        {
            Turn();
        }
    }

    private void FixedUpdate(){
        RaycastHit2D groundInfo = Physics2D.Raycast(groundController.position, Vector2.down, groundDistance);
        // RaycastHit2D wallInfo = Physics2D.Raycast(wallController.position, Vector2.right, wallDistance);

        // Aplica velocidad en la dirección actual
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (!groundInfo){
            Turn();
        }
    }

    private void Turn(){
        // Cambia la dirección del movimiento y gira al personaje
        moveRight = !moveRight;
        transform.eulerAngles = new Vector3(0, moveRight ? 0 : 180, 0);
        speed = moveRight ? Mathf.Abs(speed) : -Mathf.Abs(speed);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * groundDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallController.transform.position, wallController.transform.position + Vector3.right * wallDistance);
    }

}
