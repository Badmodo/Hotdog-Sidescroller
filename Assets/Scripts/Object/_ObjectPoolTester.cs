using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ObjectPoolTester : MonoBehaviour
{
    ObjectPoolTypeB pool;

    void Start()
    {
        pool = ObjectPoolTypeB.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCube();
        }
    }

    void SpawnCube ()
    {
        pool.Spawn(Vector3.zero);
    }
}
