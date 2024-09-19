using System.Collections;
using System.Collections.Generic;
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
        if ((EnemyHealth -= amount) == 0)
        {
            enemyInvoker.SetState(EnemyState.Death);
            OnEnemyDead();
        }
    }

    private void OnEnemyDead()
    {
        // Вызов события OnEnemyDeath и возврат объекта врага в пул
        WaveManager.Instance.OnEnemyDeath();
        ObjectPooler.Instance.ReturnToPool(PoolId, gameObject);
    }

    public void OnObjectSpawn()
    {
        // Сброс состояния врага при появлении из пула
        if (enemyAI != null)
        {
            enemyAI.ResetState();
        }
        else
        {
            Debug.LogError("EnemyAI component is missing.");
        }
    }
}