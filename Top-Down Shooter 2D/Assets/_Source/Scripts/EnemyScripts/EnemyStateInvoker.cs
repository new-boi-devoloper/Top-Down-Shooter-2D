using System;
using UnityEngine;
using _Source.Scripts.Managers;

public class EnemyStateInvoker : MonoBehaviour
{
    public static Action<EnemyState> OnEnemyStateChange;
    
    public void SetEnemyState(EnemyState newEnemyState)
    {
        OnEnemyStateChange?.Invoke(newEnemyState);
    }
}