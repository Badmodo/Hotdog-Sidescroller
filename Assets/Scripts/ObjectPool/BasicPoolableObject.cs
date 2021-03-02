using UnityEngine;
using System.Collections;

public class BasicPoolableObject : MonoBehaviour, IPoolable
{
    protected ObjectPool pool;


    public virtual void Activation()
    {

    }

    public virtual void Despawn()
    {
        pool.ReturnToPool(gameObject);
    }

    public virtual void InitialSpawn(ObjectPool pool)
    {
        this.pool = pool;
    }

}