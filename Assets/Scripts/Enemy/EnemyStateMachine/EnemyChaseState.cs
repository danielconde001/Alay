using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine p_enemyStateMachine)
    { 
        Transform playerTransform = PlayerManager.Instance.GetPlayer().transform;
        p_enemyStateMachine.GetComponent<Pathfinding.AIPath>().canMove = true;
        p_enemyStateMachine.AIDestinationSetter.target = playerTransform;

        Animator animator = p_enemyStateMachine.GetComponent<Animator>();
        animator.SetBool("IsMoving", true);
    }

    public override void UpdateState(EnemyStateMachine p_enemyStateMachine)
    {
        Transform playerTransform = PlayerManager.Instance.GetPlayer().transform;
        float distanceToPlayer = Vector2.Distance(p_enemyStateMachine.transform.position, playerTransform.position);

        p_enemyStateMachine.SpriteRenderer.flipX =
            (p_enemyStateMachine.transform.position.x - playerTransform.position.x) < 0;

        if (distanceToPlayer < p_enemyStateMachine.AggroRange)
        {
            p_enemyStateMachine.SwitchState(p_enemyStateMachine.attackState);
        }
    }
}
