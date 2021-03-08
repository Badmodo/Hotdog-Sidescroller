using System.Collections;
using UnityEngine;

public class MustardGrenadierEnemy : Goomba
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shootCooldown = 2.5f;
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
        while(true)
        {
            yield return new WaitForSeconds(shootCooldown);

            Vector3 vel = rb.velocity;
            rb.velocity = Vector3.zero;

            yield return new WaitForSeconds(.5f);
            animator.SetBool("OnAttack", true);

            Shoot(firePoint.position);

            yield return new WaitForSeconds(.5f);
            animator.SetBool("OnAttack", false);

            rb.velocity = vel;
        }
    }

    void Shoot(Vector3 particlePosition)
    {
        poolManager.SpawnBouncingBullet(particlePosition, firePoint.rotation);
    }
}
