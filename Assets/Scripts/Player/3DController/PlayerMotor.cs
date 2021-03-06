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

    public float hangTime = .2f;
    private float hangCounter;

    public LayerMask enemyLayer;
    public GUIStyle gui;

    //Reference
    Rigidbody rb;
    PlayerController3D player;
    Collider collider;
    PlayerFeedback feedback;
    PlayerRaycaster raycaster;

    //Status
    Vector2 targetVelocity = Vector2.zero;
    Vector2 currentVelocity = Vector2.zero;

    bool onGround;
    bool prev_OnGround;
    bool hasAirJump = true;
    bool isMoving;
    bool prev_IsMoving;

    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmission;

    public ParticleSystem impactEffect;
    private bool wasOnGround;

    #region Mono
    void Awake()
    {
        //Ref
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController3D>();
        raycaster = GetComponent<PlayerRaycaster>();
        collider = GetComponent<Collider>();
    }

    void Start()
    {
        feedback = player.Feedback;
        footEmission = footsteps.emission;
    }

    private void Update()
    {
        // show footstep effects
        if(Input.GetAxisRaw("Horizontal") != 0 && onGround == true)
        {
            footEmission.rateOverTime = 30;
        }
        else
        {
            footEmission.rateOverTime = 0;
        }

        // Show ground impact effect
        if(!wasOnGround && onGround)
        {
            impactEffect.gameObject.SetActive(true);
            impactEffect.Stop();
            impactEffect.transform.position = footsteps.transform.position;
            impactEffect.Play();
        }
        wasOnGround = onGround;

        //gives kyote time
        if (onGround)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 220, 200, 20), "On ground = " + onGround, gui);
        GUI.Label(new Rect(20, 240, 200, 20), "Y = " + rb.velocity.y.ToString("000"), gui);
        GUI.Label(new Rect(20, 260, 200, 20), "hasAirJump = " + hasAirJump, gui);
        GUI.Label(new Rect(20, 280, 200, 20), "currentVelocity = " + currentVelocity, gui);
        GUI.Label(new Rect(20, 300, 200, 20), "targetVelocity = " + targetVelocity, gui);
        GUI.Label(new Rect(20, 320, 200, 20), "raycaster.OnGround = " + raycaster.OnGround, gui);
    }
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

        AnimationUpdate();
        prev_OnGround = onGround;
        prev_IsMoving = isMoving;
    }

    public void TickFixedUpdate()
    {
        ExecuteVelocity();
    }
    #endregion

    void AnimationUpdate ()
    {
        //set jump animation
        if (prev_OnGround && !onGround)
        {
            feedback.SetJumpAnimation(true);
            SfxPlayer.instance.Play_PlayerJump();
        }
        else if (!prev_OnGround && onGround)
        {
            feedback.SetJumpAnimation(false);
            SfxPlayer.instance.Play_PlayerWalk();
        }

        //Horizontal movement
        if (prev_IsMoving && !isMoving)
            feedback.SetWalkAnimation(false);
        else if (!prev_IsMoving && isMoving)
            feedback.SetWalkAnimation(true);

        //feedback.SetitJesus();
        //feedback.SetitJesus2();

    }

    #region OnGround
    void UpdateOnGroundStatus()
    {
        onGround = !isJumping ? raycaster.OnGround : false;

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
            feedback.SetVelocityY(targetVelocity.y);
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

        isMoving = targetVelocity.x > 0.1f || targetVelocity.x < -0.1f;
    }



    void DetectJumpCommand()
    {
        if (PressedJump)
        {
            //implamentation of kyote time
            if (!isJumping && hangCounter > 0f) 
            {
                Jump(jumpPower);
            }
            else if (hasAirJump)
            {
                hasAirJump = false;
                Jump(jumpPower);
            }
        }

        if (isJumping && ReleasedJumpButton)
        {
            targetVelocity.y = targetVelocity.y * 0.5f;
            //targetVelocity.y = 0f;
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
        if (!isJumping)
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

    bool ReleasedJumpButton => (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W)
            || Input.GetKeyUp(KeyCode.UpArrow));
    bool isJumping => targetVelocity.y > .1f;
    #endregion
}