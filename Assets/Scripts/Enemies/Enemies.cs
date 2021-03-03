using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    protected bool isJumpable = true;

    [SerializeField] protected int attackStrength = 1;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    /// <summary>Death method for enemies. Destroys the enemy gameObject.</summary>
    public void JumpedOn()
    {
        SfxPlayer.instance.Play_EnemyHurt();
        Destroy(gameObject);
    }
}