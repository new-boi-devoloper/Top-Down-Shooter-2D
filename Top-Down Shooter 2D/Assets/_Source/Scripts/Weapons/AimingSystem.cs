using UnityEngine;
using Input = UnityEngine.Input;

public class MousePositionTracker : MonoBehaviour
{
    private Vector3 _mousePosition;
    private float _offset = 90;

    private void Update()
    {
        // Получаем позицию курсора мыши в мировых координатах
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + _offset);
    }

    public Vector3 GetMousePosition()
    {
        // Возвращаем актуальную позицию курсора мыши
        return _mousePosition;
    }
}