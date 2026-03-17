using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack { get => enemyAttack; }

    [SerializeField] private float playerSearchRange;
    public float PlayerSearchRange { get => playerSearchRange; }

    [SerializeField] private float maxChaseRange;
    public float MaxChaseRange { get => maxChaseRange; }

    [SerializeField] private float aggroRange;
    public float AggroRange { get => aggroRange; }

    private AIDestinationSetter aiDestinationSetter;
    public AIDestinationSetter AIDestinationSetter { get => aiDestinationSetter; }

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; }

    private EnemyBaseState currentState;
    public EnemyBaseState CurrentState
    {
        get => currentState;
        set => currentState = value;
    }

    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyFleeState fleeState = new EnemyFleeState();
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();

    private void Awake()
    {
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentState = idleState;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState p_newState)
    {
        currentState = p_newState;
        currentState.EnterState(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerSearchRange);
    }
}
