using UnityEngine;
using System.Collections;

public class EnemyBullet_Standard : EnemyBulletBase
{
    bool canBeDestroyByEnemy; //Can be destroyed by other enemies

    public override void Activation()
    {
        base.Activation();
        StartCoroutine(DelayBeforeActivatingFriendlyCollision());
    }

    public override void Despawn()
    {
        base.Despawn();
        canBeDestroyByEnemy = false;
    }

    private IEnumerator DelayBeforeActivatingFriendlyCollision ()
    {
        yield return new WaitForSeconds(.5f);
        canBeDestroyByEnemy = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBeDestroyByEnemy && GameLayers.IsTargetOnEnemyLayer(other.gameObject))
        {
            Despawn();
        }

        if (GameLayers.IsTargetOnGroundLayer(other.gameObject))
        {
            Despawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canBeDestroyByEnemy && GameLayers.IsTargetOnEnemyLayer(collision.gameObject))
        {
            Despawn();
        }

        if (GameLayers.IsTargetOnGroundLayer(collision.gameObject))
        {
            Despawn();
        }
    }
}