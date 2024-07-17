using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementOnPlatform : MonoBehaviour
{
    [SerializeField] private float speed, distance;
    [SerializeField] private Transform groundController;
    [SerializeField] private bool moveRight;

    private Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        RaycastHit2D groundInfo = Physics2D.Raycast(groundController.position, Vector2.down, distance);

        rb.velocity = new Vector2(speed, rb.velocity.y);

        // if (!groundInfo)
        if (groundInfo == false)
        {
            //Turn
            Turn();
        }
        
    }

    private void Turn(){
        moveRight = !moveRight;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * distance);
    }

}
