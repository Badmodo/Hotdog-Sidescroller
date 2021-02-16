using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was created to attach to the hotdog, which is treated as a 3D gameobject inorder
// to interact with 3D environmental gameObjects and differs from the 2D character controller we 
//Had before

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ScoreSystem))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerFeedback))]
public class PlayerController3D : MonoBehaviour
{
    const float HurtInvulnerabilityDuration = 1f;

    public static PlayerController3D Instance;

    //References
    PlayerHealth playerHealth;
    PlayerMotor motor;
    PlayerFeedback feedback;
    UIManager uiManager;

    //Status
    float timeLeft = 120;
    public static int Score { get; private set; }
    float hurtInvulTimer = -1;

    public void DamagePlayer()
    {
        if (!InHurtInvulnerability())
        {
            StartHurtInvulnerabilityTimer();
            feedback.EnterDamageBlink(HurtInvulnerabilityDuration);
            playerHealth.TakeDamage();
        }
    }

    #region MonoBehavior
    void Awake()
    {
        Instance = this;
        playerHealth = GetComponent<PlayerHealth>();
        motor = GetComponent<PlayerMotor>();
    }

    void Start()
    {
        uiManager = UIManager.Instance;
        uiManager.SetScore(Score);
    }

    void Update()
    {
        motor.TickUpdate();

        TickTimer();
    }

    void FixedUpdate()
    {
        motor.TickFixedUpdate();
    }

    void OnCollisionStay(Collision collision)
    {
        if (!InHurtInvulnerability() && collision.gameObject.tag == "Enemy")
        {
            CollidedWithEnemy(collision.collider);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            AddScore(10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            AddScore(100);
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region Public
    public void SteppedOnEnemy(Collider enemyCollider)
    {
        motor.SteppedOnEnemy();
        enemyCollider.GetComponent<EnemyController>()?.SteppedOnByPlayer();
    }
    #endregion

    #region Collision
    void CollidedWithEnemy(Collider enemYCollider)
    {
        if (motor.IsAboveTarget(enemYCollider))
        {
            AddScore(100);
            enemYCollider.gameObject.GetComponent<EnemyController>()?.SteppedOnByPlayer();
            motor.SteppedOnEnemy();
        }
        else
        {
            DamagePlayer();
        }
    }
    #endregion

    #region Score
    void AddScore(int amount)
    {
        Score += amount;
        uiManager.SetScore(Score);
    }
    #endregion

    #region Hurt timer
    void StartHurtInvulnerabilityTimer()
    {
        hurtInvulTimer = HurtInvulnerabilityDuration;
        StartCoroutine(InvulnerbilityTimerCountdown());
    }

    IEnumerator InvulnerbilityTimerCountdown()
    {
        while (hurtInvulTimer > 0f)
        {
            hurtInvulTimer -= Time.deltaTime;
            yield return null;
        }
    }
    #endregion

    #region Timer
    void TickTimer ()
    {
        timeLeft -= Time.deltaTime;
        uiManager.SetTime((int)timeLeft);
        if (timeLeft < 0.1f)
        {
            LevelLoader.LoadGameplayLevel();
        }
    }
    #endregion

    #region Helpers

    bool InHurtInvulnerability() => hurtInvulTimer > 0f;
    #endregion
}