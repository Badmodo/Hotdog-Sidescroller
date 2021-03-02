using UnityEngine;
using System.Collections;

public abstract class EnemyBodyBase : EnemyBase
{
    public bool canKilledByJumpingOnThem = true;

    public void SteppedOnByPlayer()
    {
        SfxPlayer.instance.Play_EnemyHurt();
        Destroy(gameObject);    
    }
}