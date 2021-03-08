using UnityEngine;
using System.Collections;

public class SpikeEnemy : EnemyBodyBase
{
    protected override void Start()
    {
        base.Start();
        isStompable = false;
    }
}