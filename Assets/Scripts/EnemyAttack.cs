using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    FlipBoss flipBoss; 


    float pursueRange = 8f;
    float AttackRange = 2f;
    float distanceBetween;
    float forceDirection;
    
    public bool isFlipped;
    public float speed = 3f;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player" ).transform;
        rb = animator.GetComponent<Rigidbody2D>();
        flipBoss = animator.GetComponent<FlipBoss>();
        forceDirection = flipBoss.isFlipped ? -1 : 1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        distanceBetween = Vector2.Distance(rb.position, player.position);
        if (distanceBetween < pursueRange)
        {
            rb.MovePosition(newPos);
        }

        if (distanceBetween < AttackRange)
        {
            rb.AddForce(new Vector2(0.5f * forceDirection , 0f), ForceMode2D.Impulse);
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
