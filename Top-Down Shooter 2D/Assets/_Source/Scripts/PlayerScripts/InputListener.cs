using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnStop;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void FixedUpdate()
    {
        Vector2 movemntInput = playerControls.Player.Move.ReadValue<Vector2>();
        if (movemntInput != Vector2.zero)
        {
            OnMove?.Invoke(movemntInput);
        }
        else
        {
            OnStop?.Invoke();
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}