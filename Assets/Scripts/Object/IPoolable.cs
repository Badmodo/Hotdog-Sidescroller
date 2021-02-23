using System.Collections;
using UnityEngine;

public interface IPoolable
{
    void InitialSpawn(ObjectPoolTypeB pool);
    void Despawn();
}