using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemies : Enemies
{
    public float enemySpeed;

    protected bool facingRight = false;

    private int moveDirection;

    private void Start() => UpdateFacing();

    private void Update() => WallhitDetection();

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
        moveDirection = spriteRenderer.flipX ? 1 : -1;
        rb.velocity = new Vector2(moveDirection, 0) * enemySpeed;
    }
}