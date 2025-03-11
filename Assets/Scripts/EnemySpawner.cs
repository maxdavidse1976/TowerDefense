using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int _enemiesToSpawn = 4;
    [SerializeField] GameObject _enemyPrefab;
    
    [SerializeField] float _minXPosition = -4f;
    [SerializeField] float _maxXPosition = 4f;
    [SerializeField] float _minZPosition = -4f;
    [SerializeField] float _maxZPosition = 4f;

    public List<Transform> Enemies;
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
            Enemies.Add(newEnemy.transform);
        }
    }
}
