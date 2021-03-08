using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goomba : EnemyBodyBase
{
    [SerializeField] private int EnemySpeed;
    [SerializeField] private bool facingRight = false;

    protected Rigidbody rb;
    protected SpriteRenderer sr;
    protected int currentMoveDir;

    protected virtual void Awake ()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        UpdateMovementDirection();
    }

    protected override void Start()
    {
        base.Start();
        isStompable = true;
    }

    private void Update()
    {
        WallhitDetection();
        UpdateMovementDirection();
    }

    private void WallhitDetection ()
    {
        Vector2 checkDir = new Vector2(currentMoveDir, 0);
        if (Physics.Raycast(transform.position, checkDir,  out RaycastHit hit, 0.8f))
        {
            if (GameLayers.IsTargetOnEnemyLayer(hit.collider.gameObject) || GameLayers.IsTargetOnGroundLayer(hit.collider.gameObject))
            Flip();
        }
        Debug.DrawRay(transform.position, checkDir * 0.8f, Color.red);
    }

    private void Flip ()
    {
        facingRight = !facingRight;
        UpdateMovementDirection();
    }

    private void UpdateMovementDirection ()
    {
        sr.flipX = facingRight;
        currentMoveDir = facingRight ? 1 : -1;
        rb.velocity = new Vector2(currentMoveDir, 0) * EnemySpeed;
    }
}
