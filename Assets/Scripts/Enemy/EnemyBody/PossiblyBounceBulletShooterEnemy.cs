using System.Collections;
using UnityEngine;

public class PossiblyBounceBulletShooterEnemy : EnemyBodyBase
{
    public Transform firePoint;
    public float delay = 2.5f;
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

        //slow enemy down to 0 speed while firing
        animator.SetBool("OnAttack", true);
        MustardBounceBullet(firePoint.position);
        yield return new WaitForSeconds(1f);
        animator.SetBool("OnAttack", false);
        StartCoroutine(FireDelay());
    }

    void MustardBounceBullet(Vector3 particlePosition)
    {
        poolManager.SpawnBouncingBullet(particlePosition, firePoint.rotation);
    }
}
