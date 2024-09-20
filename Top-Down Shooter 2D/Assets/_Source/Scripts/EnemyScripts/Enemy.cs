using _Source.Scripts.Managers;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] private EnemyData enemyData;

    internal EnemyAI enemyAI;
    private EnemyInvoker enemyInvoker;
    public float EnemyHealth { get; private set; }

    public int PoolId { get; set; }

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyInvoker = new EnemyInvoker(this);

        EnemyHealth = enemyData.health;
        PoolId = enemyData.enemyId;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (enemyAI != null)
        {
            enemyAI.Initialize(enemyData, enemyInvoker);
        }
        else
        {
            Debug.LogError("EnemyAI component is missing.");
        }
    }

    public void ChangeHealth(float amount)
    {
        EnemyHealth -= amount;
        Debug.Log($"Took hit, remaining health: {EnemyHealth}");

        if (EnemyHealth <= 0)
        {
            enemyInvoker.SetState(EnemyState.Death);
            OnEnemyDead();
        }
    }

    private void OnEnemyDead()
    {
        ObjectPooler.Instance.ReturnToPool(PoolId, gameObject);
        WaveManager.Instance.OnEnemyDeath();
    }

    public void OnObjectSpawn()
    {
        if (enemyAI != null)
        {
            enemyAI.ResetState();
        }
        else
        {
            Debug.LogError("EnemyAI component is missing.");
        }

        EnemyHealth = enemyData.health;
    }
}