using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts;
using Triggers;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionDuration;


    internal float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GroundSurface groundSurface))
        {
            GameObject explosionGO = Instantiate(explosionPrefab);
            explosionGO.transform.position = transform.position;
            ExplosionTrigger explosionTrigger = explosionGO.GetComponent<ExplosionTrigger>();
            explosionTrigger.damage = damage;
            Destroy(gameObject);
            Destroy(explosionGO, explosionDuration);
        }
    }
}
