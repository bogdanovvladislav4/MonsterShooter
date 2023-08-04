using System;
using GameScripts;
using PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyScripts
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private Player player;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float radiusSpawn;
        [SerializeField]private float baseHealthValue;

        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float delayBetweenSpawn;
        [SerializeField] private float freezingSpawnDuration;
        [SerializeField] private GameObject freezingSpawnInfo;
        [SerializeField] private GameObject freezingAllEnemiesInfo;

        private bool _isSpawnFreezing;

        private float _currentSpawnDelay;
        private float _lastTimeSpawn;
        private float _startFreezingSpawn;
    

        private void Start()
        {
            _currentSpawnDelay = delayBetweenSpawn;
        }

        private void Update()
        {
            if (Time.time - _lastTimeSpawn > _currentSpawnDelay && !_isSpawnFreezing)
            {
                SpawnOneEnemy();
            }

            if (_isSpawnFreezing)
            {
                StartFreezingSpawn();
                if (Time.time - _startFreezingSpawn > freezingSpawnDuration)
                {
                    EndFreezingSpawn();
                }
            }
        }
    
        public void AddFreezingSpawn()
        {
            _isSpawnFreezing = true;
            _startFreezingSpawn = Time.time;
        }

        public float GetBaseHealthValue()
        {
            return baseHealthValue;
        }
        private Vector3 RandomSpawnPlace()
        {
            var position = player.transform.position;
            var rndCos = Random.Range(-180, 180);
            var rndSin = Random.Range(-180, 180);
        
            var cos = MathF.Cos(rndCos);
            var sin = MathF.Sin(rndSin);
            float x = position.x + radiusSpawn * cos;
            float z = position.z + radiusSpawn * sin;
            return new Vector3(x, 0, z);
        }

        private void SpawnOneEnemy()
        {
            _lastTimeSpawn = Time.time;
            if (_currentSpawnDelay > minSpawnDelay)
            {
                BoostSpawn();
            }
            else
            {
                _currentSpawnDelay = minSpawnDelay;
            }
            GameObject enemy = Instantiate(enemyPrefab);
            enemyPrefab.transform.position = RandomSpawnPlace();
            Enemy e = enemy.GetComponent<Enemy>();
            e.player = player;
            BoostEnemyHealth();
            e.SetHealth(baseHealthValue);
            e.game = game;
            e.playerDamageDetected = player.GetDamagePlayer();
            e.freezingAllEnemiesInfo = freezingAllEnemiesInfo;
            game.AddNewEnemy(enemy, e);
        }

        private void BoostEnemyHealth()
        {
            if ((int)Time.time % 10 == 0)
            {
                baseHealthValue += 10;
            }
        }

        private void BoostSpawn()
        {
            if ((int)Time.time % 10 == 0)
            {
                _currentSpawnDelay -= 1;
            }
        }
        
        private void StartFreezingSpawn()
        {
            _currentSpawnDelay = int.MaxValue;
            freezingSpawnInfo.SetActive(true);
        }
    
        private void EndFreezingSpawn()
        {
            _isSpawnFreezing = false;
            _currentSpawnDelay = delayBetweenSpawn;
            freezingSpawnInfo.SetActive(false);
        }
    }
}
