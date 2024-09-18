using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] private EnemyData enemyData;

    internal EnemyAI enemyAI;
    private EnemyInvoker enemyInvoker;

    public int PoolId { get; set; }

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyInvoker = new EnemyInvoker(this);

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