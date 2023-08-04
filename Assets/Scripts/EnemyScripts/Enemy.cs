using System.Collections;
using GameScripts;
using PlayerScripts;
using Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator enemyAnimator;
        [SerializeField] private float movementDelay;
        [SerializeField] private float minDistanceToPlayer;
        [SerializeField] private float damage;
        [SerializeField] private GameObject freezingEffects;
        [SerializeField] private float freezingDuration;
        [SerializeField] private float hitAnimationDuration;


        private bool _isDead;
        private bool _isFreezingEffect;
        private float _distanceToPlayer;
        private float _startFreezingEnemy;
        private float _healthValue;
        private float _hitAnimationTime;

        internal EnemyHitTrigger hitTrigger;
        internal Player player;
        internal GameObject playerDamageDetected;
        internal ExplosionTrigger explosionTrigger;
        internal Game game;
        internal GameObject freezingAllEnemiesInfo;

        private void Start()
        {
            _isDead = false;
            transform.LookAt(player.transform);
            StartCoroutine(EnemyCoroutine());
        }

        void Update()
        {
            if (hitTrigger != null)
            {
                AddDamage(hitTrigger.damage);
                TakeDamage();
                hitTrigger = null;
            }

            if (explosionTrigger != null)
            {
                AddDamage(explosionTrigger.damage);
                TakeDamage();
                explosionTrigger = null;
            }
            _distanceToPlayer = Vector3.Distance(transform.position, playerDamageDetected.transform.position);
            CheckDistanceToPlayer();
            CheckHealthValue();

            if (_isFreezingEffect)
            {
                StartFreezingEffect();
                if (Time.time - _startFreezingEnemy > freezingDuration)
                {
                    EndFreezingEffect();
                }
            }
            if(Time.time - _hitAnimationTime > hitAnimationDuration)
            {
                MovementTowardsPlayer();
            }
            else
            {
                EnemyStopped();
            }
        }

        public void SetHealth(float health)
        {
            _healthValue = health;
        }

        public float GetDamage()
        {
            return damage;
        }

        IEnumerator EnemyCoroutine()
        {
            yield return new WaitForSeconds(movementDelay);
            MovementTowardsPlayer();
        }
        

        public void AddDamage(float damage)
        {
            _healthValue -= damage;
        }
        public void AddFreezingEffects()
        {
            _isFreezingEffect = true;
            _startFreezingEnemy = Time.time;

        }

        public float GetHealth()
        {
            return _healthValue;
        }

        private void CheckHealthValue()
        {
            if (_healthValue <= 0)
            {
                enemyAnimator.SetTrigger("TakeDamage");
                _isDead = true;
                game.AddEnemyKilled();
                enemyAnimator.SetBool("IsDead", _isDead);
                Destroy(this);
                Destroy(gameObject, 3);
            }
        }
        
        private void TakeDamage()
        {
            enemyAnimator.SetTrigger("TakeDamage");
            _hitAnimationTime = Time.time;

        }

        private void MovementTowardsPlayer()
        {
            enemyAnimator.SetFloat("Horizontal", 1);
            enemyAnimator.SetFloat("Vertical", 1);
            agent.SetDestination(playerDamageDetected.transform.position);
            agent.speed = 3;
        }
        
        private void EnemyStopped()
        {
            enemyAnimator.SetFloat("Horizontal", 0);
            enemyAnimator.SetFloat("Vertical", 0);
            agent.speed = 0;
        }

        private void CheckDistanceToPlayer()
        {
            if (_distanceToPlayer < minDistanceToPlayer)
            {
                enemyAnimator.SetTrigger("Attack");
                enemyAnimator.SetFloat("Horizontal", Mathf.Lerp(0,1,_distanceToPlayer));
                enemyAnimator.SetFloat("Vertical", Mathf.Lerp(0,1,_distanceToPlayer));
                agent.destination = transform.position;
            }
        }

        private void StartFreezingEffect()
        {
            freezingEffects.SetActive(true);
            agent.destination = transform.position;
            enemyAnimator.speed = 0;
            freezingAllEnemiesInfo.SetActive(true);
        }
    

        private void EndFreezingEffect()
        {
            _isFreezingEffect = false;
            freezingEffects.SetActive(false);
            enemyAnimator.speed = 1;
            MovementTowardsPlayer();
            freezingAllEnemiesInfo.SetActive(false);
        }

        
    }
}