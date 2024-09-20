using System;
using _Source.Scripts.ScriptedInstruments;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Source.Scripts.PlayerScripts
{
    public class InputListener : Singleton<InputListener>
    {
        [SerializeField] private WeaponParent weaponParent;
        [SerializeField] private Pistol weaponPrefab;
        public event Action<Vector2> OnMove;
        public event Action OnStop;
        public event Action OnFire;

        private PlayerControls playerControls;

        protected override void Awake()
        {
            base.Awake();
        
            playerControls = new PlayerControls();
            playerControls.Player.Fire.performed += ctx => OnFire?.Invoke();
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

            if (Input.GetMouseButtonDown(0))
            {
                weaponPrefab.Attack();
            }

            Vector2 pointerInput = GetPointerInut();
            weaponParent.PointerPosition = pointerInput;
        }

        private Vector2 GetPointerInut()
        {
            Vector3 mousePos = playerControls.Player.Look.ReadValue<Vector2>();
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
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
}