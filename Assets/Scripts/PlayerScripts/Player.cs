using System.Collections.Generic;
using GameScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject aim;
        [SerializeField] private List<GameObject> gunsGo = new List<GameObject>();
        [SerializeField] private float health;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private Game game;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private GameObject damageDetected;
        [SerializeField] private float hitEffectDuration = 1;
        [SerializeField] private float delayBetweenShot;
        [SerializeField] private float speedShotBoosterDuration;
        [SerializeField] private GameObject speedShotBoosterInfo;
        [SerializeField] private AudioSource hitAudio;
    
        private List<Gun> _guns = new List<Gun>();
        private float _lastTimeShot;
        private float _currentDelayBetweenShot;
        private float _startSpeedShotBooster;
        private bool isSpeedShotBooster;

        internal EnemyHandAttack enemyHandAttacks;
        internal bool playerLosing;
        internal bool pressedAttackButton;

        void Start()
        {
            playerLosing = false;
            isSpeedShotBooster = false;
            _currentDelayBetweenShot = delayBetweenShot;
            foreach (var gunGo in gunsGo)
            {
                _guns.Add(gunGo.GetComponent<Gun>());
            }
        }

        void Update()
        {
            if (pressedAttackButton && Time.time - _lastTimeShot > _currentDelayBetweenShot)
            {
                _guns[Random.Range(0, _guns.Count - 1)].Shot(projectilePrefab, aim, projectileSpeed);
                _lastTimeShot = Time.time;
                pressedAttackButton = false;
            }

            if (enemyHandAttacks != null)
            {
                TakeDamage(enemyHandAttacks.GetEnemy().GetDamage());
                enemyHandAttacks = null;
            }
            CheckHealthValue();

            if (isSpeedShotBooster)
            {
                StartSpeedShotBooster();
                if (Time.time - _startSpeedShotBooster > speedShotBoosterDuration)
                {
                    EndSpeedShotBooster();   
                }
            }
        }

        public void SpeedShotBooster()
        {
            isSpeedShotBooster = true;
            _startSpeedShotBooster = Time.time;
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetDelayBetweenShot()
        {
            return delayBetweenShot;
        }

        public float GetCurrentTimeReloadGuns()
        {
            return Time.time - _lastTimeShot;
        }

        private void TakeDamage(float damage)
        {
            health -= damage;
            GameObject hit = Instantiate(hitEffect);
            hit.transform.position = damageDetected.transform.position;
            Destroy(hit, hitEffectDuration);
            hitAudio.Play();
        }

        private void StartSpeedShotBooster()
        {
            _currentDelayBetweenShot = 0;
            speedShotBoosterInfo.SetActive(true);
        }
    
        private void EndSpeedShotBooster()
        {
            isSpeedShotBooster = false;
            _currentDelayBetweenShot = delayBetweenShot;
            speedShotBoosterInfo.SetActive(false);
        }
    

        private void CheckHealthValue()
        {
            if (health <= 0)
            {
                game.PlayerDied();
                playerLosing = true;
            }
        }

        public GameObject GetDamagePlayer()
        {
            return damageDetected;
        }
    
    }
}
