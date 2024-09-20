using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private GunData gunInfo;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float offset;

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Set z to 0 for 2D

        // Calculate the direction to the mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Calculate the angle to rotate towards
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the gun towards the mouse position
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Attack()
    {
        GameObject newProjectile =
            Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);

        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.UpdateStats(gunInfo.bulletSpeed, gunInfo.weaponDamage, gunInfo.weaponRange,
                projectileSpawnPoint.position);
        }
        else
        {
            Debug.LogError("Projectile component not found on the instantiated projectile.");
        }
    }

    public GunData GetGunInfo()
    {
        return gunInfo;
    }
}