using System.Collections;
using UnityEngine;

public class PossiblyBounceBulletShooterEnemy : EnemyBodyBase
{
    public Transform firePoint;
    public float delay = 1.5f;

    protected override void Start()
    {
        base.Start();
        Shoot();
    }

    void Shoot()
    {
        StartCoroutine(FireDelay());
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(delay);
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        MustardBounceBullet(firePoint.position);
        StartCoroutine(FireDelay());
    }

    void MustardBounceBullet(Vector3 particlePosition)
    {
        poolManager.SpawnBouncingBullet(particlePosition, firePoint.rotation);
    }
}
