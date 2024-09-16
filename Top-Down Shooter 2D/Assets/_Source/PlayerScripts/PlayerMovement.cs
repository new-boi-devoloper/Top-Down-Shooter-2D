using UnityEngine;

public class PlayerMovement
{
    public void Move(Rigidbody2D playerRb, Vector2 direction, float playerSpeed)
    {
        Vector2 moveDirection = direction * playerSpeed;
        playerRb.velocity = moveDirection;
    }

    public void Stop(Rigidbody2D playerRb)
    {
        playerRb.velocity /= 1.05f;
    }

    public void RotateHorizontal(Rigidbody2D playerRb, float playerRotation, float rotation)
    {
        playerRb.transform.Rotate(0, 0, -rotation * playerRotation);
    }
}