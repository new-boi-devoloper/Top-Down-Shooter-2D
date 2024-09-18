using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private Transform attackZone;

    private EnemyAI enemyAI;
    private float lastAttackTime;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.OnAttackStateEntered += HandleAttackStateEntered;
        }
    }

    private void OnDestroy()
    {
        if (enemyAI != null)
        {
            enemyAI.OnAttackStateEntered -= HandleAttackStateEntered;
        }
    }

    private void HandleAttackStateEntered(EnemyData enemyData)
    {
        Attack(enemyData.attackPower, enemyData.attackRate);
    }

    private void Attack(float attackPower, float attackRate)
    {
        if (Time.time - lastAttackTime >= attackRate)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackZone.position, attackRadius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    Player player = hitCollider.GetComponent<Player>();
                    if (player != null)
                    {
                        player.ChangeHealth(-attackPower);
                    }
                }
            }

            lastAttackTime = Time.time;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackZone != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackZone.position, attackRadius);
        }
    }
}