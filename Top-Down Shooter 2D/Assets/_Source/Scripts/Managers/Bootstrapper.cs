using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputListener inputListener;

    private PlayerInvoker playerInvoker;
    private PlayerMovement playerMovement;

    public static Bootstrapper Instance { get; private set; }

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

        playerMovement = new PlayerMovement();
        playerInvoker = new PlayerInvoker(player, playerMovement);
        playerInvoker.Subscribe(inputListener);
    }

    private void OnDestroy()
    {
        playerInvoker.Unsubscribe(inputListener);
    }
}