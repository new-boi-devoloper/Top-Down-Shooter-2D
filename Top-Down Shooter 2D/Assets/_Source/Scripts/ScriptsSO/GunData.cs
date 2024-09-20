using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "GunSO/GunData", order = 1)]
public class GunData : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public int weaponDamage;
    public float weaponRange;
    public float bulletSpeed;
}