using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeGrainMustardEnemy : EnemyBodyBase
{
    public Transform firePoint;
    public float delay = 1.5f;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected override void Start()
    {
        base.Start();
        isStompable = true;
        Shoot();
    }


    void Shoot()
    {
        StartCoroutine(FireDelay());
    }

    IEnumerator FireDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            animator.SetTrigger("IsAttcking");
            yield return new WaitForSeconds(0.7f);
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        poolManager.SpawnBullet(firePoint.position, firePoint.rotation);
    }
}
