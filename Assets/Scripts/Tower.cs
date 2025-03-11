using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform _enemy;
    [SerializeField] List<Transform> _enemies;

    [SerializeField] Transform _towerHead;

    [Header("Attack details")]
    [SerializeField] float _attackRange = 3f;
    [SerializeField] float _lastTimeAttacked;
    [SerializeField] float _attackCooldown;

    [Header("Bullet details")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed = 3f;
    
    void Update()
    {
        if (_enemy == null)
        {
            _enemy = ClosestEnemy();
            return;
        }

        if (EnemyInRange())
        {
            _towerHead.LookAt(_enemy);
            if (ShootingNotOnCooldown())
            {
                ShootBullet();
            }
            else
            {
                Debug.Log("Attack is on cooldown");
            }
        }
    }

    Transform ClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;
        
        foreach (Transform enemy in _enemies)
        {
            float distanceToTower = Vector3.Distance(enemy.position, transform.position);
            if (distanceToTower < closestDistance && distanceToTower <= _attackRange)
            {
                closestDistance = distanceToTower;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            _enemies.Remove(closestEnemy);
        }

        return closestEnemy;
    }

    void FindRandomEnemy()
    {
        if (_enemies.Count <= 0) return;

        int randomIndex = Random.Range(0, _enemies.Count);
        _enemy = _enemies[randomIndex];
        _enemies.RemoveAt(randomIndex);
    }

    bool ShootingNotOnCooldown()
    {
        if (Time.time > _lastTimeAttacked + _attackCooldown)
        {
            _lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

    bool EnemyInRange()
    {
        return Vector3.Distance(_enemy.position, _towerHead.position) < _attackRange;
    }

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _towerHead.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().linearVelocity = (_enemy.position - _towerHead.position).normalized * _bulletSpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
