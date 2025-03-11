using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Tower : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] Transform _towerHead;

    [Header("Attack details")]
    [SerializeField] float _attackRange = 3f;
    [SerializeField] float _lastTimeAttacked;
    [SerializeField] float _attackCooldown;

    [Header("Bullet details")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed = 3f;

    Transform _enemy;

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
        
        foreach (Transform enemy in _enemySpawner.Enemies)
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
            _enemySpawner.Enemies.Remove(closestEnemy);
        }

        return closestEnemy;
    }

    void FindRandomEnemy()
    {
        if (_enemySpawner.Enemies.Count <= 0) return;

        int randomIndex = Random.Range(0, _enemySpawner.Enemies.Count);
        _enemy = _enemySpawner.Enemies[randomIndex];
        _enemySpawner.Enemies.RemoveAt(randomIndex);
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
