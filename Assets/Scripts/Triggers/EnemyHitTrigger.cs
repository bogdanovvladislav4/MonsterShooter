using System;
using System.Collections;
using System.Collections.Generic;
using EnemyScripts;
using Triggers;
using UnityEngine;

public class EnemyHitTrigger : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float hitEffectDuration;

    internal float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            damage = projectile.damage;
            enemy.hitTrigger = this;
            GameObject hitEffect = Instantiate(hitEffectPrefab);
            hitEffect.transform.position = transform.position;
            Destroy(projectile.gameObject);
            Destroy(hitEffect, hitEffectDuration);
        }

        if (other.TryGetComponent(out ExplosionTrigger explosionTrigger))
        {
            enemy.explosionTrigger = explosionTrigger;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            enemy.hitTrigger = null;
        }
        if (other.TryGetComponent(out ExplosionTrigger explosionTrigger))
        {
            enemy.explosionTrigger = null;
        }
    }
}
