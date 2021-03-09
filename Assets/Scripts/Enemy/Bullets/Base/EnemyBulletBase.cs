using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBulletBase : EnemyBase, IPoolable
{
    public float speed = 10f;
    public float aliveDuration = 2f;
    protected Rigidbody rb;
    protected ObjectPool pool;
    protected bool Destroyed;

    float aliveTimer;

    public virtual void InitialSpawn(ObjectPool pool)
    {
        this.pool = pool;
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Activation()
    {
        Destroyed = false;
        StartCoroutine(DelayedAutoDespawn());
        aliveTimer = aliveDuration;
    }

    public virtual void Despawn()
    {
        if (!Destroyed)
        {
            Destroyed = true;
            pool.ReturnToPool(gameObject);
            aliveTimer = -1f;
        }
    }

    public override void DamagedPlayer()
    {
        Despawn();
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
