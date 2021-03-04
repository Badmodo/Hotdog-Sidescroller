using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionEnemy : Enemies
{
    [SerializeField] public Collider aoeCollider;
    public GameObject AOEParticle;

    Animator animator;


    private void Start()
    {
        //aoeCollider = GetComponent<Collider>();
        //aoeCollider.isTrigger = false;
        StartCoroutine(Sad());
        animator = GetComponent<Animator>();

    }
    IEnumerator Sad()
    {
        aoeCollider.enabled = true;
        isJumpable = false;
        AOEParticle.SetActive(true);
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(3f);

        StartCoroutine(Mad());
    }
    IEnumerator Mad()
    {
        aoeCollider.enabled = false;
        isJumpable = true;
        AOEParticle.SetActive(false);
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(3f);

        StartCoroutine(Sad());
    }
}