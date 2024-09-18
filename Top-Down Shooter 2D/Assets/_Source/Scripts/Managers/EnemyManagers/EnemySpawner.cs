using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<int> enemyIds; // Список ID врагов для спавна

    private List<Transform> _spawnPoints; // Список точек спавна

    private void Start()
    {
        _spawnPoints = new List<Transform>();

        // Заполняем список дочерними объектами
        foreach (Transform child in transform)
        {
            _spawnPoints.Add(child);
        }

        // Проверка на пустоту списка
        if (_spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points found. Please add child objects to the EnemySpawner.");
        }
    }

    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomId = PickRandomId();
            Vector2 randomPosition = PickRandomPosition();
            ObjectPooler.Instance.SpawnFromPool(randomId, randomPosition, Quaternion.identity);
        }
    }

    private int PickRandomId()
    {
        return enemyIds[Random.Range(0, enemyIds.Count)];
    }

    private Vector2 PickRandomPosition()
    {
        if (_spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points available.");
            return Vector2.zero; // Возвращаем нулевой вектор, чтобы избежать ошибки
        }

        Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        return randomSpawnPoint.position;
    }
}