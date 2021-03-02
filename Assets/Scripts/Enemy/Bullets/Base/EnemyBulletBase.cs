using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBulletBase : EnemyBase, IPoolable
{
    public float speed = 10f;
    public float aliveDuration = 2f;
    protected Rigidbody rb;
    protected ObjectPool pool;

    float aliveTimer;

    public virtual void InitialSpawn(ObjectPool pool)
    {
        this.pool = pool;
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Activation()
    {
        StartCoroutine(DelayedAutoDespawn());
        aliveTimer = aliveDuration;
    }

    public virtual void Despawn()
    {
        pool.ReturnToPool(gameObject);
        aliveTimer = -1f;
    }

    IEnumerator DelayedAutoDespawn()
    {
        //Not 100% sure
        while (aliveTimer > 0f)
        {
            if ((aliveTimer -= Time.deltaTime) <= 0)
                Despawn();
            yield return null;
        }
    }
}
