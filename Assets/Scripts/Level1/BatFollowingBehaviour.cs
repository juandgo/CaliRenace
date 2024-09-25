using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFollowingBehaviour : StateMachineBehaviour
{
    [SerializeField] private float speedMovement;
    [SerializeField] private float baseTime;
    private float followTime;
    private Transform player;
    private BatFollow batFollow;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followTime =baseTime;
    //    player = GameObject.FindGameObjectsWithTag("Player").GetComponent<Transform>();
       player = GameObject.FindWithTag("Player").GetComponent<Transform>();
       batFollow = animator.gameObject.GetComponent<BatFollow>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position,speedMovement * Time.deltaTime);
       batFollow.Turn(player.position);
       followTime -= Time.deltaTime;
       if(followTime <= 0){
            animator.SetTrigger("BatBack");
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position,speedMovement * Time.deltaTime);
       batFollow.Turn(player.position);
       followTime -= Time.deltaTime;
       if(followTime <= 0){
            animator.SetTrigger("BatBack");
       }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
