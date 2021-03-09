using System.Collections;
using UnityEngine;

public class MustardGrenadierEnemy : Goomba
{
    const float ShootAngle = 45f;

    [SerializeField] Transform shootPointOffsetRaw;
    [SerializeField] float shootCooldown = 2.5f;
    Animator animator;

    Vector3 shootPositionOffset;
    Quaternion shootRotation;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        shootPositionOffset = shootPointOffsetRaw.transform.localPosition;
    }

    protected override void Start()
    {
        base.Start();
        isStompable = true;
        UpdateShootDirection();
        UpdateShootRotation();
        StartCoroutine(FireDelay());
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(Random.Range(0f, 4f));

        while (true)
        {
            yield return new WaitForSeconds(shootCooldown);

            //Stand still
            Vector3 walkingVelocity = rb.velocity;
            StandStill();

            //Play mustard charge up attack
            ToggleAnimation(true);
            yield return new WaitForSeconds(.3f);
            Shoot();

            yield return new WaitForSeconds(.2f);
            ToggleAnimation(false);
            rb.velocity = walkingVelocity;
        }
    }

    void Shoot()
    {
        poolManager.SpawnMustardGrenadeBullet(transform.position + shootPositionOffset, shootPointOffsetRaw.rotation);
    }

    void StandStill()
    {
        rb.velocity = Vector3.zero;
        //Debug.Log(rb.velocity);
    }

    void ToggleAnimation(bool isTrue)
    {
        animator.SetBool("OnAttack", isTrue);
    }

    protected override void FlipDirection()
    {
        base.FlipDirection();
        UpdateShootRotation();
        UpdateShootDirection();
    }

    void UpdateShootRotation()
    {
        float angle = facingRight ? ShootAngle : 180 - ShootAngle;
        shootPointOffsetRaw.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.DrawRay(shootPointOffsetRaw.position, shootPointOffsetRaw.right * 100f, Color.grey, 30f);
        //Quaternion rotation = Quaternion.Euler(0f, 0f, angle) * transform.right;
    }

    void UpdateShootDirection ()
    {
        if (facingRight)
            shootPositionOffset.x = Mathf.Abs(shootPositionOffset.x);
        else
            shootPositionOffset.x = -Mathf.Abs(shootPositionOffset.x);
        shootPositionOffset.x = 0;
    }
}
