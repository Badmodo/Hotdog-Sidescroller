using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIPoolableImplementer : MonoBehaviour,  IPoolable
{
    public void Despawn()
    {
    }

    public void InitialSpawn(ObjectPoolTypeB pool)
    {
    }
}