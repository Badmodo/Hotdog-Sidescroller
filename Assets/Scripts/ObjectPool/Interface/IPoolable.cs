using System.Collections;
using UnityEngine;

public interface IPoolable
{
    void InitialSpawn(ObjectPool pool);
    void Activation();
    void Despawn();
}