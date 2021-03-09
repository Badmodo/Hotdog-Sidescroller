using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goomba : EnemyBodyBase
{
    [SerializeField] protected float MoveSpeed;
    [SerializeField] protected bool facingRight = false;

    protected Rigidbody rb;
    protected SpriteRenderer sr;
    protected Vector3 moveDir;
    protected Vector3 offset_top;
    protected Vector3 offset_bot;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();

        sr.flipX = facingRight;
        moveDir = new Vector3(MoveSpeed * (facingRight ? 1 : -1), 0f, 0f);

        Collider collider = GetComponent<Collider>();
        float extent = collider.bounds.extents.y - 0.2f;
        offset_top = new Vector3(0f, extent, 0f);
        offset_bot = new Vector3(0f, -extent, 0f);

        ExecuteVelocity();
    }

    protected override void Start()
    {
        base.Start();
        isStompable = true;
    }

    private void Update()
    {
        WallhitDetection();
        
        //UpdateMovementDirection();
    }

    private void WallhitDetection()
    {
        Vector2 checkDir = moveDir;
        if (Physics.Raycast(transform.position + offset_top, checkDir, out RaycastHit hitT, 0.8f))
        {
            DetectedObjectInfront(hitT.collider.gameObject);
        }
        else if (Physics.Raycast(transform.position + offset_bot, checkDir, out RaycastHit hitB, 0.8f))
        {
            DetectedObjectInfront(hitB.collider.gameObject);
        }
        Debug.DrawRay(transform.position + offset_top, checkDir * 0.8f, Color.red);
        Debug.DrawRay(transform.position + offset_bot, checkDir * 0.8f, Color.blue);
    }

    private void DetectedObjectInfront(GameObject go)
    {
        //GameLayers.IsTargetOnEnemyLayer(go) || 
        if (GameLayers.IsTargetOnGroundLayer(go))
        {
            FlipDirection();
        }
    }

    protected virtual void FlipDirection ()
    {
        facingRight = !facingRight;
        sr.flipX = facingRight;
        moveDir.x = facingRight ? Mathf.Abs(moveDir.x) : -Mathf.Abs(moveDir.x);
        ExecuteVelocity();
    }

    public override void DamagedPlayer()
    {
        ExecuteVelocity();
    }

    protected void ExecuteVelocity () => rb.velocity = moveDir * MoveSpeed;
}
