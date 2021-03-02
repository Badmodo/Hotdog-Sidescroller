using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected bool softHead = true;

    public int EnemySpeed;
    public bool facingRight = false;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int moveDirection;

    private void Start() => UpdateFacing();

    private void Update() => WallhitDetection();

    /// <summary>Death method for enemies. Destroys the enemy gameObject.</summary>
    public void JumpedOn()
    {
        SfxPlayer.instance.Play_EnemyHurt();
        Destroy(gameObject);
    }

    /// <summary>Uses Raycasts to detect if there's a wall or nearby, then moves appropriatley.</summary>
    private void WallhitDetection()
    {
        Vector2 checkDir = new Vector2(moveDirection, 0);
        if (Physics.Raycast(transform.position, checkDir, out RaycastHit hit, 0.6f))
        {
            GameObject obiect = hit.collider.gameObject;
            if (GameLayers.IsTargetOnEnemyLayer(obiect) || GameLayers.IsTargetOnGroundLayer(obiect)) Flip();
        }
        Debug.DrawRay(transform.position, checkDir * 0.6f, Color.red);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        UpdateFacing();
    }

    private void UpdateFacing()
    {
        spriteRenderer.flipX = facingRight;
        moveDirection = facingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection, 0) * EnemySpeed;
    }
}
