using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using PlayerScripts;
using UnityEngine;

public class EnemyHandAttack : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerDamageDetected playerTakeDamage))
        {
            playerTakeDamage.GetPlayer().enemyHandAttacks = this;
            Debug.Log("Player take damage");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerDamageDetected playerTakeDamage))
        {
            playerTakeDamage.GetPlayer().enemyHandAttacks = null;
        }
    }

    public Enemy GetEnemy()
    {
        return enemy;
    }
}
