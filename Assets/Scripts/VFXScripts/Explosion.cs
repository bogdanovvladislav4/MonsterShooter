using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject[] explosionEffects;
    [SerializeField] private Light explosionLight;
    [SerializeField] private float maxIntensiveExplosionLight;
    [SerializeField] private float minIntensiveExplosionLight;
    [SerializeField] private float explosionDuration;
    
}
