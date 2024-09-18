using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    private void Start()
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
        enemySpawner.SpawnEnemies(5);
    }
}