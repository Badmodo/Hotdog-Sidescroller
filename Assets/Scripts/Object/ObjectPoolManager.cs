using System.Collections;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    public GameObject pf_Particle;
    public GameObject pf_B;
    public GameObject pf_C;

    ObjectPoolTypeB pool_Particle;
    ObjectPoolTypeB pool_B;
    ObjectPoolTypeB pool_C;

    private void Awake()
    {
        Instance = this;
        pool_Particle = new ObjectPoolTypeB(pf_Particle);
        pool_B = new ObjectPoolTypeB(pf_B);
        //pool_C = new ObjectPoolTypeB(pf_C);
    }

    public void SpawnParticle (Vector3 pos)
    {
        //pool_Particle.Spawn(pos);
    }
}