using System.Collections;
using System.Collections.Generic;
using _Source.Scripts.Managers;
using UnityEngine;

public class EnemyInvoker
{
    private Enemy enemy;

    public EnemyInvoker(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void SetState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                enemy.enemyAI.SetIdle();
                break;
            case EnemyState.Roaming:
                enemy.enemyAI.SetRoaming();
                break;
            case EnemyState.Chasing:
                enemy.enemyAI.SetChasing();
                break;
            case EnemyState.Attacking:
                enemy.enemyAI.SetAttacking();
                break;
            case EnemyState.Death:
                enemy.enemyAI.SetDeath();
                break;
        }
    }
}