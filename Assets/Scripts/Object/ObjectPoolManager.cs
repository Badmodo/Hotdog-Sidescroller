using System.Collections;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public GameObject pf_Particle;
    public GameObject pf_Bullet;
    public GameObject pf_C;

    ObjectPoolTypeB pool_Particle;
    ObjectPoolTypeB pool_Bullet;
    ObjectPoolTypeB pool_C;

    private void Awake()
    {
        Instance = this;
        pool_Particle = new ObjectPoolTypeB(pf_Particle, transform);
        pool_Bullet = new ObjectPoolTypeB(pf_Bullet, transform);
        //pool_C = new ObjectPoolTypeB(pf_C);
    }

    public void SpawnEnemyDeathParticle (Vector3 pos, Quaternion rotation)
    {
        pool_Particle.Spawn(pos, rotation);

    } public void SpawnBullet (Vector3 pos, Quaternion rotation)
    {
        pool_Bullet.Spawn(pos, rotation);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        SpawnEnemyDeathParticle(Vector3.zero);
    //    }
    //}
}