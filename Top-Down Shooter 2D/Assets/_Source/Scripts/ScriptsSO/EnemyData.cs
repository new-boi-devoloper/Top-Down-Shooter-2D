using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemySO/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Management")]
    public int enemyId;

    [Header("Enemy stats")]
    public float speed;
    public float attackPower;
    public float health;

    [Header("AI Settings")]
    public float roamingDistanceMax = 7f;
    public float roamingDistanceMin = 3f;
    public float roamingTimerMax = 2f;
    public bool isChasingEnemy = true;
    public float chasingDistance = 4f;
    public float chasingSpeedMultiplier = 2f;
    public bool isAttackingEnemy = true;
    public float attackingDistance = 2f;
    public float attackRate = 2f;
}