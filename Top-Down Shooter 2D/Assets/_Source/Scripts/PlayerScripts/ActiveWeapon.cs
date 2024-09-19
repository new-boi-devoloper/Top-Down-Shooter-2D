using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private GameObject CurrentActiveWeapon;

    private PlayerControls playerControls;
    private float timeBetweenAttacks;
    private float attackCooldownTimer;

    private bool attackButtonDown, isAttacking = false;

    #region Singleton

    public static ActiveWeapon Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public ActiveWeapon(PlayerControls playerControls)
    {
        this.playerControls = playerControls;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Player.Fire.started += _ => StartAttacking();
        playerControls.Player.Fire.canceled += _ => StopAttacking();

        AttackCooldown();
        NewWeapon(CurrentActiveWeapon);
    }

    private void Update()
    {
        Attack();
        UpdateAttackCooldown();
    }

    public void NewWeapon(GameObject newWeapon)
    {
        // if (newWeapon != null)
        // {
        //     // newWeapon = TryGetComponent<IWeapon>();
        // }
        //
        // CurrentActiveWeapon = newWeapon;
        //
        // AttackCooldown();
        // timeBetweenAttacks = CurrentActiveWeapon.GetGunInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        attackCooldownTimer = timeBetweenAttacks;
    }

    private void UpdateAttackCooldown()
    {
        if (isAttacking)
        {
            attackCooldownTimer -= Time.deltaTime;
            if (attackCooldownTimer <= 0)
            {
                isAttacking = false;
            }
        }
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon != null)
        {
            AttackCooldown();
            // CurrentActiveWeapon.Attack();
        }
    }
}