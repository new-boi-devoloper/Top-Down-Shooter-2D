using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    public int remainingEnemies { get; private set; }

    #region Singleton

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void OnEnable()
    {
        // Подписываемся на событие инициализации ObjectPooler
        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.OnPoolInitialized += OnPoolInitialized;
        }
        else
        {
            Debug.LogError("ObjectPooler instance is not set. Please ensure ObjectPooler is properly initialized.");
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.OnPoolInitialized -= OnPoolInitialized;
        }
    }

    private void OnPoolInitialized()
    {
        // Вызываем спавн врагов после инициализации ObjectPooler
        if (enemySpawner != null)
        {
            Debug.Log("5");
            enemySpawner.SpawnEnemies(5);
            Debug.Log("6");
        }
    }

    public void OnEnemyDeath()
    {
        remainingEnemies--;
        Debug.Log($"Enemy died. Remaining enemies: {remainingEnemies}");
    }
}