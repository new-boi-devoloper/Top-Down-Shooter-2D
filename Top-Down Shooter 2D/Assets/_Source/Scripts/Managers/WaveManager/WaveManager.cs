using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int amountToSpawn = 8; // Количество врагов для спавна
    [SerializeField] private float initializationDelay = 0.2f; // Время задержки перед 

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
            Debug.Log("2");
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
            SpawnNewWave();
        }
    }

    public void OnEnemyDeath()
    {
        remainingEnemies--;
        Debug.Log($"Enemy died. Remaining enemies: {remainingEnemies}");

        if (remainingEnemies <= 0)
        {
            SpawnNewWave();
        }
    }

    private void SpawnNewWave()
    {
        remainingEnemies = amountToSpawn;
        enemySpawner.SpawnEnemies(amountToSpawn);
    }
}