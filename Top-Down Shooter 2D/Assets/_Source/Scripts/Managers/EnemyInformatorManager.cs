using UnityEngine;

public class EnemyInformatorManager : MonoBehaviour
{
    public static EnemyInformatorManager Instance { get; private set; }

    public Vector3 PlayerPosition { get; private set; }

    private GameObject player;

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

    private void Start()
    {
        // Находим игрока при старте
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found with tag 'Player'");
        }
        else
        {
            Debug.Log("Player found and initialized.");
        }
    }

    private void Update()
    {
        // Обновляем позицию игрока, если player не равен null
        if (player != null)
        {
            PlayerPosition = player.transform.position;
        }
        else
        {
            Debug.LogError("Player is null.");
        }
    }
}