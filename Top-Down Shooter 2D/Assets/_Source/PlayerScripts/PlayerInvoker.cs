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
        inputListener.OnRotateHorizontal += OnRotateHorizontal;
    }

    public void Unsubscribe(InputListener inputListener)
    {
        inputListener.OnMove -= OnMove;
        inputListener.OnStop -= OnStop;
        inputListener.OnRotateHorizontal -= OnRotateHorizontal;
    }

    private void OnMove(Vector2 direction)
    {
        _playerMovement.Move(_player.Rb, direction, _player.PlayerSpeed);
    }

    private void OnStop()
    {
        _playerMovement.Stop(_player.Rb);
    }

    private void OnRotateHorizontal(float rotation)
    {
        _playerMovement.RotateHorizontal(_player.Rb, _player.PlayerRotation, rotation);
    }
}