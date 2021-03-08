using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Goomba
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 1.5f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AttackCycle());
    }

    private IEnumerator AttackCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootCooldown);
            Shoot();
        }
    }

    private void Shoot()
    {
        poolManager.SpawnBombBullet(firePoint.position, firePoint.rotation);
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