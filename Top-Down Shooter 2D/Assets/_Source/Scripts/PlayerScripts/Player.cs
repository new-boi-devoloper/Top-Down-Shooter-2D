using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: Header("Player Stats")]
    
    [field: SerializeField] public float PlayerSpeed { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
}