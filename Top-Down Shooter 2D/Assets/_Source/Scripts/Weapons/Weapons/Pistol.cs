using UnityEngine;
using UnityEngine.Serialization;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private GunData gunInfo;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(projectilePrefab, projectileSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(gunInfo.weaponRange);
    }

    public GunData GetGunInfo()
    {
        return gunInfo;
    }
}