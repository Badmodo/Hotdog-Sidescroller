using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    public float speed = 10;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        StartCoroutine(StopMovement());
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(19f);
        GetComponent<BossMovement>().enabled = false;
    }
}

