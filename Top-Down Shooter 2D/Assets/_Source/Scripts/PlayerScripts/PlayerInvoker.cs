using System;
using UnityEngine;

public class PlayerInvoker
{
    private Player _player;
    private PlayerMovement _playerMovement;

    public PlayerInvoker(Player player, PlayerMovement playerMovement)
    {
        this._player = player;
        this._playerMovement = playerMovement;
    }

    public void Subscribe(InputListener inputListener)
    {
        inputListener.OnMove += OnMove;
        inputListener.OnStop += OnStop;
    }

    public void Unsubscribe(InputListener inputListener)
    {
        inputListener.OnMove -= OnMove;
        inputListener.OnStop -= OnStop;
    }

    private void OnMove(Vector2 direction)
    {
        _playerMovement.Move(_player.Rb, direction, _player.PlayerSpeed);
    }

    private void OnStop()
    {
        _playerMovement.Stop(_player.Rb);
    }
}