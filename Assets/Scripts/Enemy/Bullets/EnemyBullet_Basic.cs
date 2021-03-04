using UnityEngine;
using System.Collections;

public class EnemyBullet_Basic : EnemyBulletBase
{

    float aliveTimer;

    public virtual void Activation()
    {
        StartCoroutine(DelayedAutoDespawn());
        aliveTimer = aliveDuration;
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