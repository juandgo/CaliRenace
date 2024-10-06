using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float distanceLine;
    public LayerMask layerPlayer;
    public bool isGoingUp = false;
    public float speedGoingUp;
    public float waitTime;
    public Animator animator;
    public int damage;

    private void Update(){
        RaycastHit2D infoPlayer = Physics2D.Raycast(transform.position, Vector3.down, distanceLine, layerPlayer);

        if(infoPlayer){
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if(isGoingUp){
            transform.Translate(Time.deltaTime * speedGoingUp * Vector3.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;

            if(isGoingUp){
                isGoingUp = false;
            }else{
                // isGoingUp = true;
                animator.SetTrigger("Hit");
                StartCoroutine(WaitingOnFloor());
            }
        }

        if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement)){
            playerMovement.TakeDamage(damage);
        }
    }

    private IEnumerator WaitingOnFloor(){
        yield return new WaitForSeconds(waitTime);
        isGoingUp = true;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceLine);
    }

}
