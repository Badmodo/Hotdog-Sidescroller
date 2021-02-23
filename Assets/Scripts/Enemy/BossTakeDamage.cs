using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        BossHealthBar.health -= 10f;
    }
}
