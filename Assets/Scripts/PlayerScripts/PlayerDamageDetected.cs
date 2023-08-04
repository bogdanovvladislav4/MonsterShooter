using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerDamageDetected : MonoBehaviour
    {
        [SerializeField] private Player player;

        public Player GetPlayer()
        {
            return player;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyHandAttack enemyHandAttack))
            {
                player.enemyHandAttacks = enemyHandAttack;
            }
        }
    }
}