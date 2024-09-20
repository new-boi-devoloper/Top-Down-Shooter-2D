using System;
using _Source.Scripts.PlayerScripts;
using UnityEngine;

public class PlayerInvoker
{
    private Player _player;
    private PlayerMovement _playerMovement;

    public PlayerInvoker(Player player, PlayerMovement playerMovement)
    {
        _player = player;
        _playerMovement = playerMovement;
    }

    public void Subscribe(InputListener inputListener)
    {
        inputListener.OnMove += OnMove;
        inputListener.OnStop += OnStop;
        inputListener.OnFire += OnFire;
    }

    public void Unsubscribe(InputListener inputListener)
    {
        inputListener.OnMove -= OnMove;
        inputListener.OnStop -= OnStop;
        inputListener.OnFire -= OnFire;
    }

    private void OnMove(Vector2 direction)
    {
        _playerMovement.Move(_player.Rb, direction, _player.PlayerSpeed);
    }

    private void OnStop()
    {
        _playerMovement.Stop(_player.Rb);
    }

    private void OnFire()
    {
        // _weapon.Shoot();
    }
}