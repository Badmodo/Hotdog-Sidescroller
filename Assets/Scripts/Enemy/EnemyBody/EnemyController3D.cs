using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController3D : EnemyBodyBase
{
    public int EnemySpeed;
    public bool facingRight = false;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    Rigidbody rb;
    private SpriteRenderer sr;
    private int moveDir;

    void Awake ()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        UpdateFacing();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        WallhitDetection();
        UpdateFacing();
    }

    void WallhitDetection ()
    {
        Vector2 checkDir = new Vector2(moveDir, 0);
        if (Physics.Raycast(transform.position, checkDir,  out RaycastHit hit, 0.8f))
        {
            if (GameLayers.IsTargetOnEnemyLayer(hit.collider.gameObject) || GameLayers.IsTargetOnGroundLayer(hit.collider.gameObject))
            Flip();
        }
        Debug.DrawRay(transform.position, checkDir * 0.8f, Color.red);
    }

   
    void Flip ()
    {
        facingRight = !facingRight;
        UpdateFacing();
    }

    void UpdateFacing ()
    {
        sr.flipX = facingRight;
        moveDir = facingRight ? 1 : -1;
        rb.velocity = new Vector2(moveDir, 0) * EnemySpeed;
    }

    
}
