using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<int> enemyIds;

    private List<Transform> _randomSpawnPositions;

    private void OnEnable()
    {
        _randomSpawnPositions = new List<Transform>();
        var allChildren = GetComponentsInChildren<Transform>();
        foreach (var child in allChildren)
        {
            if (child != transform)
            {
                _randomSpawnPositions.Add(child);
            }
        }

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
            return;
        }

        if (_randomSpawnPositions == null || _randomSpawnPositions.Count == 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int randomId = PickRandomId();
            Transform randomPosition = PickRandomPosition();

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
            return null;
        }

        int randomIndex = Random.Range(0, _randomSpawnPositions.Count);
        return _randomSpawnPositions[randomIndex];
    }
}