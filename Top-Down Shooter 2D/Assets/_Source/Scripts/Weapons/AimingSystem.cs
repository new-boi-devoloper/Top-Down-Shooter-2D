using UnityEngine;
using UnityEngine.InputSystem;

public class AimingSystem : MonoBehaviour
{
    [SerializeField] private float pointOffset = 2f; // Расстояние от игрока до точки прицеливания
    [SerializeField] private GameObject aimPointPrefab; // Префаб точки прицеливания

    private GameObject aimPointInstance;
    private Camera mainCamera;
    private Transform playerTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = transform;
        aimPointInstance = Instantiate(aimPointPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - playerTransform.position).normalized;
        Vector3 aimPosition = playerTransform.position + direction * pointOffset;

        aimPointInstance.transform.position = aimPosition;
    }

    public Vector3 GetAimPosition()
    {
        return aimPointInstance.transform.position;
    }
}