using UnityEngine;
using System.Collections;

public class EnemyBullet_Translate : EnemyBulletBase
{
    public override void Activation ()
    {
        base.Activation();
        rb.velocity = transform.right * speed;
    }
}