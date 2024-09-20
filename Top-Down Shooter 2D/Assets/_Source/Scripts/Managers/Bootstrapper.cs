using _Source.Scripts.PlayerScripts;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputListener inputListener;

    private PlayerInvoker playerInvoker;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerControls playerControls;
    private ActiveWeapon activeWeapon;

    public static Bootstrapper Instance { get; private set; }
    public PlayerCombat PlayerCombat => playerCombat;

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

        playerControls = new PlayerControls();
        playerCombat = new PlayerCombat();
        playerMovement = new PlayerMovement();
        playerInvoker = new PlayerInvoker(player, playerMovement);

        playerInvoker.Subscribe(inputListener);
    }

    private void OnDestroy()
    {
        playerInvoker.Unsubscribe(inputListener);
    }
}