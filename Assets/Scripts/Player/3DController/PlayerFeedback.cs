using System.Collections;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    const float BlinkInterval = 0.1f;

    //Reference
    [SerializeField] SpriteRenderer sr;
    Animator animator;



    //Cache
    Color colorHide = new Color(1, 1, 1, 0);
    #region MonoBehavior

    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        if (moveX > 0.1f)
        {
            FaceLeft(false);
        }
        else if (moveX < -0.1f)
        {
            FaceLeft(true);
        }

        //Debug
        if (Input.GetKeyDown(KeyCode.Y))
        {
            EnterDamageBlink(1f);
        }

        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsWalking", false);
        }


    }

    #region Public
    public void FaceLeft (bool facingLeft)
    {
        sr.flipX = facingLeft;
    }

    public void EnterDamageBlink (float duration) => StartCoroutine(Blink(duration));
    #endregion

    IEnumerator Blink (float duration)
    {
        bool reveal = false;
        
        while (duration > 0f)
        {
            sr.color = reveal ? Color.white : colorHide;
            reveal = !reveal;
            duration -= BlinkInterval;
            yield return new WaitForSeconds(BlinkInterval);
        }
        sr.color = Color.white;
    }
}