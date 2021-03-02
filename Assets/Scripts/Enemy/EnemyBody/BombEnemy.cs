using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : EnemyBodyBase
{
    public Transform firePoint;
    public float delay = 1.5f;

    protected override void Start()
    {
        base.Start();
        Shoot();
    }

    void Shoot()
    {
        StartCoroutine(FireDelay());
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(delay);
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        MustardBombBullet(firePoint.position);
        StartCoroutine(FireDelay());
    }

    void MustardBombBullet(Vector3 particlePosition)
    {
        poolManager.SpawnBombBullet(particlePosition, firePoint.rotation);
    }
}


/*
 
public class BombEnemy : EnemyBodyBase
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
        //StartCoroutine(SetInactive());
    }

    //IEnumerator SetInactive()
    //{
    //    Bomb.SetActive(true);
    //    yield return new WaitForSeconds(0.15f);       
    //    Destroy(gameObject);
    //}
}

 */