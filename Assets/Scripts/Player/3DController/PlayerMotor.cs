using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController3D))]
[RequireComponent(typeof(PlayerRaycaster))]
public class PlayerMotor : MonoBehaviour
{
    const int MaxJumps = 2;

    public int moveSpeed = 400;
    public int jumpPower = 1250;
    public int gravity = 250;
    public float horizontalAcceleration = 40f;

    public LayerMask enemyLayer;

    //Reference
    Rigidbody rb;
    PlayerController3D player;
    Collider collider;

    //Status
    bool hasAirJump = true;

    Vector2 targetVelocity = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;
    bool onGround;
    public PlayerRaycaster raycaster { get; private set; }

    #region Mono
    void Awake()
    {
        //Ref
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController3D>();
        raycaster = GetComponent<PlayerRaycaster>();
        collider = GetComponent<Collider>();
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 200, 20), "On ground = " + onGround);
    //    GUI.Label(new Rect(20, 40, 200, 20), "rb.velocity = " + rb.velocity);
    //    GUI.Label(new Rect(20, 60, 200, 20), "Falling = " + Falling);
    //    GUI.Label(new Rect(20, 80, 200, 20), "currentVelocity = " + currentVelocity);
    //    GUI.Label(new Rect(20, 100, 200, 20), "targetVelocity = " + targetVelocity);
    //}
    #endregion

    #region Public 
    public bool IsAboveTarget(Collider target) => (collider.bounds.min.y - 0.1f) > (target.bounds.min.y);
    public void SteppedOnEnemy()
    {
        Jump(jumpPower);
    }

    public void TickUpdate()
    {
        UpdateOnGroundStatus();
        ApplyGravity();
        DetectJumpCommand();
        CheckForEnemyBelowToStepOn();
        HorizontalMoveUpdate();
    }

    public void TickFixedUpdate()
    {
        ExecuteVelocity();
    }
    #endregion

    #region OnGround

    void UpdateOnGroundStatus()
    {
        if (Falling)
            onGround = raycaster.OnGround;
        else
            onGround = false;

        if (onGround)
        {
            hasAirJump = true;
        }
    }

    void ApplyGravity()
    {
        if (onGround)
        {
            if (targetVelocity.y < 0)
                targetVelocity.y = 0f;
        }
        else
        {
            targetVelocity.y -= gravity * Time.deltaTime;
        }
    }
    #endregion

    #region Movement
    void HorizontalMoveUpdate()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !raycaster.AgainstLeft)
        {
            targetVelocity.x = -1;
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !raycaster.AgainstRight)
        {
            targetVelocity.x = 1;
        }
        else
        {
            targetVelocity.x = 0f;
        }
    }

    void DetectJumpCommand()
    {
        if (PressedJump)
        {
            if (Falling && onGround)
            {
                Jump(jumpPower);
            }
            else if (hasAirJump)
            {
                hasAirJump = false;
                Jump(jumpPower);
            }
        }
    }

    void ExecuteVelocity()
    {
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, targetVelocity.x * moveSpeed, horizontalAcceleration * Time.deltaTime);
        rb.velocity = new Vector3(currentVelocity.x, targetVelocity.y) * Time.deltaTime;
    }
    #endregion

    #region Interaction
    void CheckForEnemyBelowToStepOn()
    {
        if (Falling)
        {
            RaycastHit L;
            if (Physics.Raycast(raycaster.BL, Vector2.down, out L, 0.1f, enemyLayer))
            {
                player.SteppedOnEnemy(L.collider);
                RaycastHit R;
                if (Physics.Raycast(raycaster.BR, Vector2.down, out R, 0.1f, enemyLayer))
                {
                    player.SteppedOnEnemy(R.collider);
                }
            }
        }
    }
    #endregion

    #region Jump
    void Jump(float jumpForce)
    {
        targetVelocity.y = jumpForce;
        onGround = false;
    }
    #endregion

    #region Helpers
    bool PressedJump => (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow));
    bool Falling => rb.velocity.y <= 0f;
    #endregion
}