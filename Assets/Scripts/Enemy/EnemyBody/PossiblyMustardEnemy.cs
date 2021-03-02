using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblyMustardEnemy : EnemyBodyBase
{
    public Transform firePoint;
    //public GameObject bulletPrefab;
    public float delay = 1.5f;


    // Update is called once per frame
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
        MustardBullet(firePoint.position);
        StartCoroutine(FireDelay());
    }

    void MustardBullet(Vector3 particlePosition)
    {
        poolManager.SpawnBullet(particlePosition, firePoint.rotation);
    }

}
