using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputListener inputListener;

    private PlayerInvoker _playerInvoker;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = new PlayerMovement();
        _playerInvoker = new PlayerInvoker(player, _playerMovement);

        _playerInvoker.Subscribe(inputListener);
    }

    private void OnDestroy()
    {
        _playerInvoker.Unsubscribe(inputListener);
    }
}