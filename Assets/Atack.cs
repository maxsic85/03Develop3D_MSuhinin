using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo
stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<TestBlend>().StateMethod(); // ֲûחמגול סממבשוםטו “It Works!”
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo
    stateInfo, int layerIndex)
    {
        Debug.Log("On Attack Exit");
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo
stateInfo, int layerIndex)
    {
        Debug.Log("On Attack Update ");
    }
    override public void OnStateMove(Animator animator, AnimatorStateInfo
    stateInfo, int layerIndex)
    {
        Debug.Log("On Attack Move ");
    }
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
    int layerIndex)
    {
        Debug.Log("On Attack IK ");
    }




















    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
