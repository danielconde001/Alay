using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private const int TRUE = 1;
    private const int FALSE = 0;

    private Animator animator;

    public override void EnterState(EnemyStateMachine p_enemyStateMachine)
    {
        p_enemyStateMachine.GetComponent<Pathfinding.AIPath>().canMove = false;
        p_enemyStateMachine.AIDestinationSetter.target = p_enemyStateMachine.transform;

        animator = p_enemyStateMachine.GetComponent<Animator>();
        animator.SetTrigger("IsAttacking");
    }

    public override void UpdateState(EnemyStateMachine p_enemyStateMachine)
    {
        if (p_enemyStateMachine.EnemyAttack.AttackRateCounter > 0)
        {
            animator.SetBool("IsMoving", false);
            p_enemyStateMachine.EnemyAttack.AttackRateCounter -= Time.deltaTime;
        }
        else
        {
            if (p_enemyStateMachine.EnemyAttack.IsAttacking == FALSE)
            {
                p_enemyStateMachine.EnemyAttack.IsAttacking = TRUE;
                animator.SetTrigger("IsAttacking");
            }
        }
    }
}