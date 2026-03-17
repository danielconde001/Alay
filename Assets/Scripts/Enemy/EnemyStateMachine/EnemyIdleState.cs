using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine p_enemyStateMachine)
    {
        p_enemyStateMachine.AIDestinationSetter.target = p_enemyStateMachine.transform;
        Animator animator = p_enemyStateMachine.GetComponent<Animator>();
        animator.SetBool("IsMoving", false);
    }

    public override void UpdateState(EnemyStateMachine p_enemyStateMachine)
    {
        Transform playerTransform = PlayerManager.Instance.GetPlayer().transform;
        float distanceToPlayer = Vector2.Distance(p_enemyStateMachine.transform.position, playerTransform.position);

        if (distanceToPlayer < p_enemyStateMachine.PlayerSearchRange)
        {
            p_enemyStateMachine.SwitchState(p_enemyStateMachine.chaseState);
        }
    }
}
