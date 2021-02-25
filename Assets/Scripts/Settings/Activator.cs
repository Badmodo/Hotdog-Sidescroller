using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public GameObject ToBeActivated;
    public int delay  = 1;

    void Start()
    {
        StartCoroutine(Trigger_CR());
    }

    private IEnumerator Trigger_CR()
    {
        yield return new WaitForSeconds(delay);
        ToBeActivated.SetActive(true);
    }

}

    
