using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was created to attach to the hotdog, which is treated as a 3D gameobject inorder
// to interact with 3D environmental gameObjects and differs from the 2D character controller we 
//Had before

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ScoreSystem))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController3D : MonoBehaviour
{
    const float HurtInvulnerabilityDuration = 1f;

    public static PlayerController3D Instance;

    public Rigidbody playerRigidbody;
    public Vector3 moveDirection;

    //References
    PlayerHealth playerHealth;
    PlayerMotor motor;
    PlayerFeedback feedback;
    UIManager uiManager;
    ObjectPoolManager poolManager;
   
    //Status
    float timeLeft = 120;
    public static int Score { get; private set; }
    public static int FinalScore => Score + (int)Instance.timeLeft * 1;
    float hurtInvulTimer = -1;

    public PlayerMotor Motor => motor;
    public PlayerFeedback Feedback => feedback;

    public Transform camTarget;

    public void DamagePlayer()
    {
        if (!InHurtInvulnerability())
        {
            StartHurtInvulnerabilityTimer();
            feedback.EnterDamageBlink(HurtInvulnerabilityDuration);
            SfxPlayer.instance.Play_PlayerHurt();

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
        feedback = GetComponentInChildren<PlayerFeedback>();
    }

    void Start()
    {
        uiManager = UIManager.Instance;
        uiManager.SetScore(Score);
        poolManager = ObjectPoolManager.Instance;
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
            Debug.DrawLine(transform.position, collision.gameObject.transform.position, Color.red, 10f);
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

    #region Enemy COllision
    public void SteppedOnEnemy(Collider enemyCollider)
    {
        DieParticle(enemyCollider.transform.position);
        motor.SteppedOnEnemy();
        AddScore(100);
        //Debug.Log("" + enemyCollider);
        EnemyBodyBase enemy = enemyCollider.GetComponent<EnemyBodyBase>();
        Debug.Log("A - " + enemy);
        if (enemy != null)
        {
            Debug.Log("B - " + enemy);
            if (enemy.canKilledByJumpingOnThem)
            {
                SteppedOnEnemy_BasicEnemy(enemy);
            }
        }

        //try
        //{
        //    //Debug.Log("" + enemyCollider);
        //    EnemyBodyBase enemy = enemyCollider.GetComponent<EnemyBodyBase>();
        //    Debug.Log("A - " + enemy);
        //    if (enemy != null)
        //    {
        //        Debug.Log("B - " + enemy);
        //        switch (enemy.EnemyType)
        //        {
        //            case EnemyTypes.EnemyType1:
        //            default:
        //                SteppedOnEnemy_BasicEnemy(enemy);
        //                break;
        //        }
        //    }
        //}
        //catch (System.Exception e)
        //{
        //    Debug.Log(e.Message);
        //    //Debug.Log(e.Message + ". Ignore for now. You might've steeping on an enemy bullet. Ideallyer there are 2 layers - enenmy body and " +
        //    //    "enemy bullet.But doing that right now will break other scenes. After adding those layers and assigning them to enemy perfabs, go to GameLayers and set up the layer collision checks.");
        //    //Debug.Log(e + ". Error, I think there is no component EnemyController on enemy object " + enemyCollider.gameObject.name);
        ////}
    }

    void SteppedOnEnemy_BasicEnemy (EnemyBodyBase enemy)
    {
        enemy.SteppedOnByPlayer();
    }
    #endregion

    #region Public

    void DieParticle(Vector3 particlePosition)
    {
        poolManager.SpawnEnemyDeathParticle(particlePosition, Quaternion.identity);
    }
    #endregion

    #region Collision
    void CollidedWithEnemy(Collider enemYCollider)
    {
        if (motor.IsAboveTarget(enemYCollider))
        {
            SteppedOnEnemy(enemYCollider);
        }
        else
        {
            moveDirection = playerRigidbody.transform.position - enemYCollider.transform.position;
            playerRigidbody.AddForce(moveDirection.normalized * 10000f);
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
    void TickTimer()
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