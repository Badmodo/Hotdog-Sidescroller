using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody rb;
    public GameObject Bomb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter(Collider other)
    {   
        StartCoroutine(SetInactive());
    }

    IEnumerator SetInactive()
    {
        Bomb.SetActive(true);
        yield return new WaitForSeconds(0.15f);       
        Destroy(gameObject);
    }
}
