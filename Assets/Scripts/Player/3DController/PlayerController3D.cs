﻿using System.Collections;
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
    public static int FinalScore => Score + (int)Instance.timeLeft * 1;
    float hurtInvulTimer = -1;

    public Rigidbody playerRigidbody;
    public Vector3 moveDirection;


    public void DamagePlayer()
    {
        if (!InHurtInvulnerability())
        {
            StartHurtInvulnerabilityTimer();
            feedback.EnterDamageBlink(HurtInvulnerabilityDuration);
            playerHealth.TakeDamage();
        }
    }

    private void knockBack(GameObject target, Vector3 direction, float length, float overTime)
    {
        direction = direction.normalized;
        StartCoroutine(knockBackCoroutine(target, direction, length, overTime));
    }

    IEnumerator knockBackCoroutine(GameObject target, Vector3 direction, float length, float overTime)
    {
        float timeleft = overTime;
        while (timeleft > 0)
        {

            if (timeleft > Time.deltaTime)
                target.transform.Translate(direction * Time.deltaTime / overTime * length);
            else
                target.transform.Translate(direction * timeleft / overTime * length);
            timeleft -= Time.deltaTime;

            yield return null;
        }

    }

    #region MonoBehavior
    void Awake()
    {
        Instance = this;
        playerHealth = GetComponent<PlayerHealth>();
        motor = GetComponent<PlayerMotor>();
        feedback = GetComponent<PlayerFeedback>();
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
        if (!InHurtInvulnerability() && (GameLayers.IsTargetOnEnemyLayer(collision.gameObject) || collision.gameObject.tag == "Enemy"))
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
    }
    #endregion

    #region Public
    public void SteppedOnEnemy(Collider enemyCollider)
    {
        //Debug.Log(" Stepped on enemy");
        motor.SteppedOnEnemy();
        AddScore(100);
        try
        {
            enemyCollider.GetComponent<EnemyController3D>().SteppedOnByPlayer();

        }
        catch (System.Exception e)
        {
            Debug.Log(e + ". Error, I think there is no component EnemyController on enemy object " + enemyCollider.gameObject.name);
        }
    }
    #endregion

    #region Collision
    void CollidedWithEnemy(Collider enemYCollider)
    { 
        {
            moveDirection = playerRigidbody.transform.position - enemYCollider.transform.position;
            playerRigidbody.AddForce(moveDirection.normalized * 10000f);
        }

        if (motor.IsAboveTarget(enemYCollider))
        {
            SteppedOnEnemy(enemYCollider);
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