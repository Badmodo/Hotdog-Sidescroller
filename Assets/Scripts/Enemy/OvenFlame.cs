using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFlame : MonoBehaviour
{
    public ParticleSystem[] pfx;
    public GameObject damageCollider;
    public int igniteDuration;
    public int extinguishDuration;

    private void Start()
    {
        StartCoroutine(BurnTime());
    }

    private IEnumerator BurnTime()
    {
        while (true)
        {
            //Turn on
            TogglePfx(true);
            yield return new WaitForSeconds(.5f);
            damageCollider.SetActive(true);
            yield return new WaitForSeconds(igniteDuration - .5f);

            //Turn off
            damageCollider.SetActive(false);
            yield return new WaitForSeconds(.5f);
            TogglePfx(false);
            yield return new WaitForSeconds(extinguishDuration - .5f);
        }
    }

    void TogglePfx(bool isTrue)
    {
        foreach (var p in pfx)
        {
            if (isTrue)
                p.Play();
            else
                p.Stop();
        }
    }
}

