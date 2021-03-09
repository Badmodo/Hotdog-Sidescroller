using UnityEngine;
using System.Collections;

public class EnemyBullet_Grenade : EnemyBullet_Standard
{
    [SerializeField] float grenadeShootSpeed = 7f;

    public override void Activation()
    {
        base.Activation();
        //Debug.Log("grenade spawned");
        //Debug.Log("has pool? " + (pool != null));
        Shoot();
    }

    public override void Despawn()
    {
        base.Despawn();
        transform.rotation = Quaternion.identity;
    }

    void Shoot ()
    {
        rb.velocity = transform.right * grenadeShootSpeed;
        //Debug.DrawRay(transform.position, rb.velocity, Color.red, 30f);
        //Debug.DrawRay(transform.position, transform.right, Color.blue, 10f);
        //Debug.Log(rb.velocity);
    }

    private void Update()
    {
        //Face moving direction
        transform.rotation = Quaternion.LookRotation(Vector3.forward,
            Quaternion.Euler(0f, 0f, 90f) * rb.velocity);
    }
}