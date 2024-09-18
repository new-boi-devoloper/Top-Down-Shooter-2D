using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [field: Header("Player Stats")]
    [field: SerializeField]
    public float PlayerSpeed { get; private set; }

    [field: SerializeField] public float PlayerHealth { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeHealth(float amount)
    {
        PlayerHealth += amount;
        if (PlayerHealth <= 0)
        {
            Debug.Log("Dead");
            //Death
        }

        Debug.Log($"Took hit. Current HP: {PlayerHealth}");
    }
}