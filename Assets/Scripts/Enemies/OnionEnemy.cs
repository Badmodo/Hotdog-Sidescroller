using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionEnemy : Enemies
{
    private Collider2D aoeCollider;

    private void Start()
    {
        aoeCollider = GetComponent<Collider2D>();
        aoeCollider.isTrigger = false;
        StartCoroutine(Sad());
    }
    IEnumerator Sad()
    {
        yield return new WaitForSeconds(3f);
        aoeCollider.enabled = true;
        isJumpable = false;
        // change animation
        // enable particle system
        StartCoroutine(Mad());
    }
    IEnumerator Mad()
    {
        yield return new WaitForSeconds(3f);
        aoeCollider.enabled = false;
        isJumpable = true;
        // change animation
        // disable particle system
        StartCoroutine(Sad());
    }
}