using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int _enemiesToSpawn = 4;
    [SerializeField] GameObject _enemyPrefab;
    
    [SerializeField] float _minXPosition = -4f;
    [SerializeField] float _maxXPosition = 4f;
    [SerializeField] float _minZPosition = -4f;
    [SerializeField] float _maxZPosition = 4f;

    List<Transform> _enemies = new List<Transform>();

    void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Vector3 enemyPosition = new Vector3(Random.Range(_minXPosition, _maxXPosition), transform.position.y, Random.Range(_minZPosition, _maxZPosition));
            GameObject newEnemy = Instantiate(_enemyPrefab, enemyPosition, Quaternion.identity);
            _enemies.Add(newEnemy.transform);
        }
    }

    public List<Transform> Enemies() => _enemies;
}
