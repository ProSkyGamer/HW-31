#region

using System.Collections;
using System.Collections.Generic;
using Runtime.Reacts;
using Runtime.Spawners;
using Runtime.Utility;
using UnityEngine;

#endregion

namespace Runtime.Enemy
{
    public class EnemyCreationService
    {
        private readonly EnemiesSpawner _enemiesSpawner;
        private readonly Enemy _enemyPrefab;
        private readonly MonoBehaviour _coroutinesRunner;

        private readonly ReactsList<Enemy> _createdEnemies = new();

        public IReadonlyReactsList<Enemy> CreatedEnemies => _createdEnemies;

        public int DeadCount { get; private set; }

        private Coroutine _spawnCoroutine;

        public EnemyCreationService(EnemiesSpawner enemiesSpawner, Enemy enemyPrefab, MonoBehaviour coroutinesRunner)
        {
            _enemiesSpawner = enemiesSpawner;
            _enemyPrefab = enemyPrefab;
            _coroutinesRunner = coroutinesRunner;
        }

        public void SummonEnemiesWithInterval(EnemySettings enemySettings, float spawnInterval,
            List<Vector3> spawnPoints)
        {
            if (_spawnCoroutine != null)
            {
                _coroutinesRunner.StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }

            _spawnCoroutine = _coroutinesRunner.StartCoroutine(SpawnEnemies(enemySettings, spawnInterval, spawnPoints));
        }

        private IEnumerator SpawnEnemies(EnemySettings settings, float interval, List<Vector3> spawnPoints)
        {
            while (true)
                foreach (Vector3 spawnPoint in spawnPoints)
                {
                    Enemy enemy = _enemiesSpawner.CreateEnemy(_enemyPrefab, spawnPoint, settings);
                    _createdEnemies.Add(enemy);

                    enemy.Destroyed += Enemy_OnDestroyed;

                    yield return new WaitForSeconds(interval);
                }
        }

        private void Enemy_OnDestroyed(MonoDestroyable destroyed)
        {
            if (destroyed is not Enemy enemy)
                return;

            DeadCount++;

            _createdEnemies.Remove(enemy);
        }

        public void StopSpawningEnemies()
        {
            _coroutinesRunner.StopCoroutine(_spawnCoroutine);

            foreach (Enemy enemy in _createdEnemies.Value)
            {
                Object.Destroy(enemy);
            }

            _spawnCoroutine = null;
        }
    }
}