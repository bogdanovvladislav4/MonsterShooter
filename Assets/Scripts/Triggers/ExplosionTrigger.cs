using System;
using EnemyScripts;
using UnityEngine;

namespace Triggers
{
    public class ExplosionTrigger : MonoBehaviour
    {

        internal float damage;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.explosionTrigger = this;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.explosionTrigger = null;
            }
        }
    }
    
}