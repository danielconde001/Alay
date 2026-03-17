using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateMachine p_enemyStateMachine);
    public abstract void UpdateState(EnemyStateMachine p_enemyStateMachine);
}
