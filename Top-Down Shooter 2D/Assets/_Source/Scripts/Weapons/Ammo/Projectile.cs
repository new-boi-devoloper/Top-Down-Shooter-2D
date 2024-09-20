using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _projectileRange;
    private float _moveSpeed;
    private float _projectileDamage;
    private Vector3 _direction;

    private Vector3 _startPosition;
    
    public void UpdateStats(float moveSpeed, float projectileDamage, float projectileRange, Vector3 position)
    {
        _moveSpeed = moveSpeed;
        _projectileDamage = projectileDamage;
        _projectileRange = projectileRange;
        _startPosition = position;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemyHealth = other.gameObject.GetComponent<Enemy>();
            if (enemyHealth)
            {
                enemyHealth.ChangeHealth(_projectileDamage);
            }
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > _projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(_direction * (Time.deltaTime * _moveSpeed));
    }
}