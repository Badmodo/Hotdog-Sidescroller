using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public int EnemySpeed;
    public int XMoveDirection;

    void Update()
    {
        CollisionDetection();
        MoveForward();
    }

    void CollisionDetection ()
    {
        Vector2 checkDir = new Vector2(XMoveDirection, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, checkDir);
        Debug.DrawRay(transform.position, new Vector2(XMoveDirection, 0), Color.red);

        if (hit && hit.distance < 0.6f)
        {
            if (hit.collider.tag == "Ground")
            {
                Flip();
            }
            else if (hit.collider.tag == "Player")
            {
                HitsPlayerCollider(hit.collider);
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
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
    }
   
    void Flip ()
    {
        if (XMoveDirection > 0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
    }
}
