using System.Collections;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public GameObject pf_Particle;
    public GameObject pf_Bullet;
    public GameObject pf_BounceBullet;
    public GameObject pf_BombBullet;

    ObjectPool pool_Particle;
    ObjectPool pool_Bullet;
    ObjectPool pool_BounceBullet;
    ObjectPool pool_BombBullet;

    private void Awake()
    {
        Instance = this;
        pool_Particle = new ObjectPool(pf_Particle, transform);
        pool_Bullet = new ObjectPool(pf_Bullet, transform);
        pool_BounceBullet = new ObjectPool(pf_BounceBullet, transform);
        pool_BombBullet = new ObjectPool(pf_BombBullet, transform);
    }

    public void SpawnEnemyDeathParticle (Vector3 pos, Quaternion rotation)
    {
        pool_Particle.Spawn(pos, rotation);

    } 
    public void SpawnBullet (Vector3 pos, Quaternion rotation)
    {
        pool_Bullet.Spawn(pos, rotation);
    }  
    
    public void SpawnBouncingBullet (Vector3 pos, Quaternion rotation)
    {
        pool_BounceBullet.Spawn(pos, rotation);
    }

    public void SpawnBombBullet(Vector3 pos, Quaternion rotation)
    {
        pool_BombBullet.Spawn(pos, rotation);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        SpawnEnemyDeathParticle(Vector3.zero);
    //    }
    //}
}