using UnityEngine;

public class PlayerCombat
{
    private float lastShotTime;

    public void Shoot(int bulletId, Vector2 position, Quaternion rotation)
    {
        // if (Time.time - lastShotTime >= ObjectPooler.Instance.GetReloadTime(bulletId))
        // {
        //     GameObject bullet = ObjectPooler.Instance.SpawnFromPool(bulletId, position, rotation);
        //     if (bullet != null)
        //     {
        //         bullet.SetActive(true);
        //         lastShotTime = Time.time;
        //     }
        // }
    }
}