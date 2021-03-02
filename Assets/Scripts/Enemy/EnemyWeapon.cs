using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    //public GameObject bulletPrefab;
    public float delay = 1.5f;

    ObjectPoolManager pool;

    // Update is called once per frame
    void Start()
    {
        pool = ObjectPoolManager.Instance;
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
        MustardBullet(firePoint.position);
        StartCoroutine(FireDelay());
    }

    void MustardBullet(Vector3 particlePosition)
    {
        pool.SpawnBullet(particlePosition, firePoint.rotation);
    }

}
