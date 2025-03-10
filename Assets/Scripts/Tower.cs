using UnityEngine;
using UnityEngine.InputSystem;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform _enemy;
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

    bool ShootingNotOnCooldown()
    {
        if (Time.time > _lastTimeAttacked + _attackCooldown) ;
        {
            _lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

    bool EnemyInRange() => _enemy != null && Vector3.Distance(_enemy.position, _towerHead.position) < _attackRange;

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
