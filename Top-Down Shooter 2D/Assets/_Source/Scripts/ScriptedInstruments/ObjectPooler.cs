using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        [FormerlySerializedAs("Id")] public int id;
        public int poolSize;
        public GameObject objectPrefab;
    }
    
    public static ObjectPooler Instance { get; private set; }

    #region Singleton

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
    
    public List<Pool> pools;

    private Dictionary<int, Queue<GameObject>> _poolDictionary;
    
    private bool _isInitialized;

    private void Start()
    {
        if (!_isInitialized)
        {
            InitializePool();
            _isInitialized = true;
        }
    }

    private void InitializePool()
    {
        _poolDictionary = new Dictionary<int, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                var obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);
                DontDestroyOnLoad(obj); // Добавляем объекты в DontDestroyOnLoad
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.id, objectPool);
        }
    }
    
    public GameObject SpawnFromPool(int id, Vector2 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(id, out var objectPool) || objectPool.Count == 0)
        {
            Debug.LogWarning("Pool with id " + id + " is empty or doesn't exist");
            return null;
        }

        var objectToSpawn = objectPool.Dequeue();
        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is null. This should not happen.");
            return null;
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        //
        // if (objectToSpawn.TryGetComponent(out IPooledObject pooledObj))
        // {
        //     pooledObj.OnObjectSpawn();
        // }

        return objectToSpawn;
    }

    public void ReturnToPool(int id, GameObject objectToReturn)
    {
        if (!_poolDictionary.ContainsKey(id))
        {
            Debug.LogWarning("Pool with id " + id + " doesn't exist");
            return;
        }

        if (objectToReturn == null)
        {
            Debug.LogError("Object to return is null. This should not happen.");
            return;
        }

        objectToReturn.SetActive(false);

        _poolDictionary[id].Enqueue(objectToReturn);
    }

}
