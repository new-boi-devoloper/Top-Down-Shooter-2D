using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnStop;
    public event Action OnFire;

    private PlayerControls playerControls;
    private AimingSystem aimingSystem;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Fire.performed += ctx => OnFire?.Invoke();
        aimingSystem = GetComponent<AimingSystem>();
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

    public Vector3 GetAimPosition()
    {
        return aimingSystem.GetAimPosition();
    }
}