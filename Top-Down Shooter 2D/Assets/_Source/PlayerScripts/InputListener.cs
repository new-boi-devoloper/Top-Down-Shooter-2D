using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action<float> OnRotateHorizontal;
    public event Action<float> OnRotateVertical;
    public event Action OnStop;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        
        // playerControls.Player.RotateHorizontal.performed += ctx => OnRotateHorizontal?.Invoke(ctx.ReadValue<float>());
        // playerControls.Player.RotateVertical.performed += ctx => OnRotateVertical?.Invoke(ctx.ReadValue<float>());
    }

    private void Update()
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