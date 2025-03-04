using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunBehaviour : StateMachineBehaviour
{
    private CatBoss CatBoss;

    private Rigidbody2D rB2D;

    [SerializeField]
    private float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        CatBoss = animator.GetComponent<CatBoss>();
        rB2D = CatBoss.rB2D;
        CatBoss.Flip();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rB2D.velocity = new Vector2(speed, rB2D.velocity.y) * animator.transform.right;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rB2D.velocity = new Vector2(0, rB2D.velocity.y);
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
