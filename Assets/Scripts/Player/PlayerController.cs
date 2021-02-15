using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterRenderer))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ScoreSystem))]
public class PlayerController : MonoBehaviour
{
    const float HurtBlinkDuration = 1f; //The invulnerability durating that occurs after taking a damage.

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    public bool isGrounded;
    public float distanceToBottomOfPlayer = 0.9f;

    //References
    CharacterRenderer characterRenderer;
    PlayerHealth playerHealth;
    ScoreSystem scoreSystem;
    Collider2D collider;

    //Status
    float hurtBlinkTimer = -1;
    float moveX;

    public void DamagePlayer()
    {
        if (!InHurtBlink())
        {
            StartHurtInvulnerabilityTimer();
            characterRenderer.PlayerDamaged();
            playerHealth.TakeDamage();
        }
    }

    void Awake()
    {
        characterRenderer = GetComponent<CharacterRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        scoreSystem = GetComponent<ScoreSystem>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        PlayerMove();
        PlayerRaycast();
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(20, 20, 200, 20), "Collider bound y: " + collider.bounds.min.y);
    }

    void PlayerMove()
    {
        //animation
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsWalking", false);
        }

        //controls
        moveX = Input.GetAxis("Horizontal");
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded == true)
        {
            Jump();
        }

        //player direction
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //physics
        //one line of code that controls the player moving around
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }


    void PlayerRaycast()
    {
        //Raycast hit itembox above you
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != null && rayUp.collider != null && rayUp.distance < 0.9f && rayUp.collider.tag == "ItemBox")
        {
            Destroy(rayUp.collider.gameObject);
        }

        //Raycast hit enemy below you
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.9f && rayDown.collider.tag == "Enemy")
        {
            CollidedWithEnemy(rayDown.collider);
            //GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        }
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.9f && rayDown.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
    }

    #region Collision
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!InHurtBlink() && collision.gameObject.tag == "Enemy")
        {
            CollidedWithEnemy(collision.collider);
        }
    }

    void CollidedWithEnemy (Collider2D enemYCollider)
    {
        if (IsPlayerAboveTarget(enemYCollider))
        {
            scoreSystem.AddScore(100);
            enemYCollider.gameObject.GetComponent<EnemyMove>()?.SteppedOnByPlayer();
            ExtraJump();
        }
        else
        {
            DamagePlayer();
        }
    }

    void ExtraJump ()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower * 3f);
        isGrounded = false;
    }

    bool IsPlayerAboveTarget (Collider2D targetCollider)
    {
        return (collider.bounds.min.y - 0.1f) > (targetCollider.bounds.min.y);
    }
    #endregion

    #region Hurt timer
    //The hurt timer prevents the player from being too rapided damaged. 
    bool InHurtBlink()
    {
        return hurtBlinkTimer > 0f;
    }

    void StartHurtInvulnerabilityTimer()
    {
        hurtBlinkTimer = HurtBlinkDuration;
        StartCoroutine(InvulnerbilityTimerCountdown());
    }

    IEnumerator InvulnerbilityTimerCountdown()
    {
        while (hurtBlinkTimer > 0f)
        {
            hurtBlinkTimer -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
