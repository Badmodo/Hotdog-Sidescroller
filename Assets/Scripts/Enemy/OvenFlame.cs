using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenFlame : MonoBehaviour
{
    public GameObject fire;
    public int ignite;
    public int extinguish;

    private void Start()
    {
        StartCoroutine(BurnTime());
    }

    private IEnumerator BurnTime()
    {
        if (fire.activeSelf == false)
        {
            yield return new WaitForSeconds(ignite);
            fire.SetActive(true);
        }
        else if (fire.activeSelf == true)
        {
            yield return new WaitForSeconds(extinguish);
            fire.SetActive(false);
        }

        StartCoroutine(BurnTime());
    }
}

