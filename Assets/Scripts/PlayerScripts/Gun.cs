using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject startProjectilePos;
    [SerializeField] private GameObject shotEffectPrefab;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private float shotEffectsDuration;
    [SerializeField] private Player player;
    [SerializeField] private AudioSource shotAudio;
    public void Shot(GameObject projectilePrefab, GameObject aim, float projectileSpeed)
    {
        shotAudio.Play();
        Vector3 aimPos = aim.transform.position;
        GameObject projectile = Instantiate(projectilePrefab, startProjectilePos.transform.position, Quaternion.identity);
        projectile.name = gameObject.name + "projectile";
        Projectile projectileComp = projectile.GetComponent<Projectile>();
        projectileComp.damage = player.GetDamage();
        GameObject shotEffects = Instantiate(shotEffectPrefab, startProjectilePos.transform);
        shotEffects.transform.position = startProjectilePos.transform.position;
        projectile.transform.LookAt(aimPos);
        Rigidbody prRig = projectile.GetComponent<Rigidbody>();
        Vector3 dest = aimPos - transform.position;
        prRig.AddForce(dest * projectileSpeed, ForceMode.Acceleration);
        gunAnimator.SetTrigger("Shot");
        Destroy(shotEffects, shotEffectsDuration);
    }
}
