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
            ToggleCryingAnimation(true);
            yield return new WaitForSeconds(0.5f);
            ToggleCryingCollision(true);
            yield return new WaitForSeconds(cryingDuration - 0.5f);

            //--- here the onion is crying---


            ToggleCryingCollision(false);
            yield return new WaitForSeconds(0.5f);
            ToggleCryingAnimation(false);
            yield return new WaitForSeconds(calmDuration - 0.5f);

            //--- here the onion is calm---
        }
    }

    private void ToggleCryingCollision (bool isAttacking)
    {
        aoeDamageZone.enabled = isAttacking;
        isStompable = !isAttacking;
    }

    private void ToggleCryingAnimation (bool isCrying)
    {
        animator.SetBool("IsAttacking", isCrying);
        foreach (var p in particles)
        {
            if (isCrying)
                p.Play();
            else
            {
                p.Stop();
            }
        }
    }
}