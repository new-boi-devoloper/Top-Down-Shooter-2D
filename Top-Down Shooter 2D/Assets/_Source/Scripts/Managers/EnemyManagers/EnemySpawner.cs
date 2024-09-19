using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<int> enemyIds; // Список ID врагов для спавна

    private List<Transform> _randomSpawnPositions; // Список позиций для спавна врагов

    private void OnEnable()
    {
        _randomSpawnPositions = new List<Transform>();
        var allChildren = GetComponentsInChildren<Transform>();
        foreach (var child in allChildren)
        {
            if (child != transform) // Исключаем родительский объект
            {
                _randomSpawnPositions.Add(child);
            }
        }

        // Проверка на пустоту списка
        if (_randomSpawnPositions.Count == 0)
        {
            Debug.LogError("No spawn points found. Please add child objects to the EnemySpawner.");
        }
        else
        {
            Debug.Log($"Found {_randomSpawnPositions.Count} spawn points.");
        }
    }

    public void SpawnEnemies(int count)
    {
        if (enemyIds == null || enemyIds.Count == 0)
        {
            Debug.LogError("No enemy IDs set. Please ensure enemy IDs are properly initialized.");
            return;
        }

        if (_randomSpawnPositions == null || _randomSpawnPositions.Count == 0)
        {
            Debug.LogError("No spawn points available. Please ensure spawn points are properly initialized.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int randomId = PickRandomId();
            Transform randomPosition = PickRandomPosition();
            if (randomPosition == null)
            {
                Debug.LogWarning("Failed to pick a random position.");
                continue;
            }

            GameObject spawnedEnemy = ObjectPooler.Instance.SpawnFromPool(randomId, randomPosition.position, Quaternion.identity);
            if (spawnedEnemy == null)
            {
                Debug.LogWarning($"Failed to spawn enemy with ID {randomId} at position {randomPosition.position}.");
            }
        }
    }

    private int PickRandomId()
    {
        return enemyIds[Random.Range(0, enemyIds.Count)];
    }

    private Transform PickRandomPosition()
    {
        if (_randomSpawnPositions == null || _randomSpawnPositions.Count == 0)
        {
            Debug.LogError("No spawn points available.");
            return null; // Возвращаем null, чтобы избежать ошибки
        }

        int randomIndex = Random.Range(0, _randomSpawnPositions.Count);
        return _randomSpawnPositions[randomIndex];
    }
}