using UnityEngine;
using System.Collections;

public abstract class EnemyBodyBase : EnemyBase
{
    protected bool isStompable = false;

    public bool IsStompable => isStompable;
    public void SteppedOnByPlayer()
    {
        if (isStompable)
        {
            SfxPlayer.instance.Play_EnemyHurt();
            Destroy(gameObject);
        }
    }
}