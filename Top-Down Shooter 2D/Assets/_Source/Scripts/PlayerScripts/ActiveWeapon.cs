using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab; // Префаб оружия
    [SerializeField] private Transform weaponSpawnPoint; // Точка появления оружия
    
    private GameObject _currentWeapon;
    
    void Start()
    {
        // Создаем оружие при старте
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        // Если оружие не создано, создаем его
        if (_currentWeapon == null)
        {
            _currentWeapon = Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
            _currentWeapon.transform.parent = weaponSpawnPoint;
        }
    }

    public void Fire()
    {
        if (_currentWeapon != null)
        {
            // Вызываем метод стрельбы у оружия
            IWeapon weaponScript = _currentWeapon.GetComponent<IWeapon>();
            if (weaponScript != null)
            {
                weaponScript.Attack();
            }
        }
    }
}