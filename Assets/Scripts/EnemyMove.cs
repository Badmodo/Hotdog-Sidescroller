using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public int EnemySpeed;
    public bool facingRight = false;

    SpriteRenderer sr;
    int moveDir;

    void Start()
    {
        UpdateMoveDir();

        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        CollisionDetection();
        MoveForward();
        UpdateFacing();
    }

    void CollisionDetection ()
    {
        Vector2 checkDir = new Vector2(moveDir, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDir);
        Debug.DrawRay(transform.position, new Vector2(moveDir, 0), Color.red);

        if (hit && hit.distance < 0.6f)
        {
            if (hit.collider.tag == "Player")
            {
                HitsPlayerCollider(hit.collider);
            }
            else
            {
                Flip();
            }
        }
    }

    void HitsPlayerCollider(Collider2D collider)
    {
        //Destroy(hit.collider.gameObject);
        SceneManager.LoadScene("Prototype 1");

        //Cause player damage
    }

    void MoveForward ()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDir, 0) * EnemySpeed;
    }
   
    void Flip ()
    {
        facingRight = !facingRight;
        UpdateMoveDir();
    }

    void UpdateFacing ()
    {
        sr.flipX = facingRight;
    }

    void UpdateMoveDir ()
    {
        if (facingRight)
        {
            moveDir = 1;
        }
        else
        {
            moveDir = -1;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 20), "facingRight " + facingRight);
        GUI.Label(new Rect(20, 40, 200, 20), "sr.flipX " + sr.flipX);
        GUI.Label(new Rect(20, 60, 200, 20), "moveDir " + moveDir);
    }
}
