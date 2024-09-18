using UnityEngine;
using UnityEngine.AI;
using _Source.Scripts.Managers;
using _Source.Scripts.ScriptedInstruments;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyState startingState;

    private EnemyData enemyData;
    private EnemyInvoker enemyInvoker;
    private NavMeshAgent navMeshAgent;
    private EnemyState currentState;
    private float roamingTimer;
    private Vector3 roamingPosition;
    private Vector3 startingPosition;

    public event Action<EnemyData> OnAttackStateEntered;

    public void Initialize(EnemyData enemyData, EnemyInvoker enemyInvoker)
    {
        this.enemyData = enemyData;
        this.enemyInvoker = enemyInvoker;
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            navMeshAgent.speed = enemyData.speed; // Устанавливаем начальную скорость
        }
        else
        {
            Debug.LogError("NavMeshAgent component is missing.");
        }

        currentState = startingState;
        roamingTimer = enemyData.roamingTimerMax;
        startingPosition = transform.position;
    }

    public void ResetState()
    {
        currentState = startingState;
        roamingTimer = enemyData.roamingTimerMax;
        startingPosition = transform.position;
        if (navMeshAgent != null)
        {
            navMeshAgent.ResetPath();
            navMeshAgent.speed = enemyData.speed; // Сбрасываем скорость
        }
        else
        {
            Debug.LogError("NavMeshAgent component is missing.");
        }
    }

    private void Update()
    {
        StateHandler();
    }

    private void StateHandler()
    {
        switch (currentState)
        {
            case EnemyState.Roaming:
                roamingTimer -= Time.deltaTime;
                if (roamingTimer < 0)
                {
                    Roaming();
                    roamingTimer = enemyData.roamingTimerMax;
                }

                CheckCurrentState();
                break;
            case EnemyState.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case EnemyState.Attacking:
                CheckCurrentState();
                break;
            case EnemyState.Death:
                break;
            default:
            case EnemyState.Idle:
                break;
        }
    }

    private void ChasingTarget()
    {
        if (EnemyInformatorManager.Instance != null)
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(EnemyInformatorManager.Instance.PlayerPosition);
                navMeshAgent.speed = enemyData.speed * enemyData.chasingSpeedMultiplier; // Ускоряемся при погоне
            }
            else
            {
                Debug.LogError("NavMeshAgent component is missing.");
            }
        }
        else
        {
            Debug.LogError("EnemyInformatorManager is not initialized.");
        }
    }

    private void CheckCurrentState()
    {
        if (EnemyInformatorManager.Instance == null)
        {
            Debug.LogError("EnemyInformatorManager is not initialized.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, EnemyInformatorManager.Instance.PlayerPosition);
        EnemyState newState = EnemyState.Roaming;

        if (enemyData.isChasingEnemy)
        {
            if (distanceToPlayer <= enemyData.chasingDistance)
            {
                newState = EnemyState.Chasing;
            }
        }

        if (enemyData.isAttackingEnemy)
        {
            if (distanceToPlayer <= enemyData.attackingDistance)
            {
                newState = EnemyState.Attacking;
                OnAttackStateEntered?.Invoke(enemyData);
            }
        }

        if (newState != currentState)
        {
            enemyInvoker.SetState(newState);
            currentState = newState;

            // Сбрасываем скорость при смене состояния
            if (newState != EnemyState.Chasing)
            {
                if (navMeshAgent != null)
                {
                    navMeshAgent.speed = enemyData.speed;
                }
                else
                {
                    Debug.LogError("NavMeshAgent component is missing.");
                }
            }
        }
    }

    private void Roaming()
    {
        startingPosition = transform.position;
        roamingPosition = GetRoamingPosition();
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(roamingPosition);
        }
        else
        {
            Debug.LogError("NavMeshAgent component is missing.");
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition +
               Utils.GetRandomDir() *
               UnityEngine.Random.Range(enemyData.roamingDistanceMin, enemyData.roamingDistanceMax);
    }

    public void SetIdle()
    {
        currentState = EnemyState.Idle;
    }

    public void SetRoaming()
    {
        currentState = EnemyState.Roaming;
    }

    public void SetChasing()
    {
        currentState = EnemyState.Chasing;
    }

    public void SetAttacking()
    {
        currentState = EnemyState.Attacking;
    }

    public void SetDeath()
    {
        currentState = EnemyState.Death;
    }
}