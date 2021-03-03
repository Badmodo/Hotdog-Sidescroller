using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardHorizontalEnemy : EnemyBodyBase
{
    public Transform firePoint;
    //public GameObject bulletPrefab;
    public float delay = 1.5f;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        animator.SetTrigger("IsAttcking");
        MustardBullet(firePoint.position);
        StartCoroutine(FireDelay());
    }

    void MustardBullet(Vector3 particlePosition)
    {
        poolManager.SpawnBullet(particlePosition, firePoint.rotation);
    }

}
