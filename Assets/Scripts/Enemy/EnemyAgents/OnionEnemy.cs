using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionEnemy : EnemyBodyBase
{
    [SerializeField] private float cryingDuration = 3f;
    [SerializeField] private float calmDuration = 3f;
    [SerializeField] private Collider aoeDamageZone;
    [SerializeField] private ParticleSystem[] particles;

    private Animator animator;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        particles = GetComponentsInChildren<ParticleSystem>();

        StartCoroutine(AttackCycle());
    }

    private IEnumerator AttackCycle()
    {
        while(true)
        {
            ToggleCryingAttack(true);
            yield return new WaitForSeconds(cryingDuration);
            ToggleCryingAttack(false);
            yield return new WaitForSeconds(calmDuration);
        }
    }

    private void ToggleCryingAttack (bool attacking)
    {
        aoeDamageZone.enabled = attacking;
        isStompable = !attacking;

        foreach (var p in particles)
        {
            if (attacking)
                p.Play();
            else
            {
                p.Stop();
            }
        }
        animator.SetBool("IsAttacking", attacking);
    }
}