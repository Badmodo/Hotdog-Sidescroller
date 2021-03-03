using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionEnemy : Enemies
{
    [SerializeField] public Collider aoeCollider;
    public GameObject AOEParticle;

    private void Start()
    {
        //aoeCollider = GetComponent<Collider>();
        //aoeCollider.isTrigger = false;
        StartCoroutine(Sad());
    }
    IEnumerator Sad()
    {
        yield return new WaitForSeconds(3f);
        aoeCollider.enabled = true;
        isJumpable = false;
        AOEParticle.SetActive(true);
        // change animation
        // enable particle system
        StartCoroutine(Mad());
    }
    IEnumerator Mad()
    {
        yield return new WaitForSeconds(3f);
        aoeCollider.enabled = false;
        isJumpable = true;
        AOEParticle.SetActive(false);
        // change animation
        // disable particle system
        StartCoroutine(Sad());
    }
}