using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float OnHurtInvulnerabilityDuration = 1f; //The invulnerability durating that occurs after taking a damage.

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    public bool isGrounded;
    public float distanceToBottomOfPlayer = 0.9f;

    //Class references
    CharacterRenderer characterRenderer;
    PlayerHealth playerHealth;

    //Status
    float hurtInvulnerabilityTimer = -1;
    float moveX;

    public void PlayerDamaged ()
    {
        if (CanPlayerBeDamaged())
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
    }

    void Update()
    {
        PlayerMove ();
        PlayerRaycast ();
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
        if (Input.GetButtonDown ("Jump") && isGrounded == true)
        {
            Jump();
        }

        //player direction
        if (moveX < 0.0f )
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f )
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

    
    void PlayerRaycast ()
    {
        //Raycast hit itembox above you
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp != null && rayUp.collider != null && rayUp.distance < 0.9f && rayUp.collider.tag == "ItemBox")
        {
            Destroy (rayUp.collider.gameObject);
        }

        //Raycast hit enemy below you
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.9f && rayDown.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        }
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.9f && rayDown.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
    }

    #region Hurt timer
    //The hurt timer prevents the player from being too rapided damaged. 


    bool CanPlayerBeDamaged()
    {
        return hurtInvulnerabilityTimer <= 0f;
    }

    void StartHurtInvulnerabilityTimer ()
    {
        hurtInvulnerabilityTimer = OnHurtInvulnerabilityDuration;
        StartCoroutine(InvulnerbilityTimerCountdown());
    }

    IEnumerator InvulnerbilityTimerCountdown()
    {
        while (hurtInvulnerabilityTimer > 0f)
        {
            hurtInvulnerabilityTimer -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion

}
