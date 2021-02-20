using System.Collections;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    const float BlinkInterval = 0.1f;
    public static bool IsJumping;

    //Reference
    [SerializeField] SpriteRenderer sr;
    Animator animator;
    PlayerController3D player;


    //Cache
    Color colorHide = new Color(1, 1, 1, 0);
    #region MonoBehavior

    #endregion
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        player = PlayerController3D.Instance;
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
        //if (moveX != 0)
        //{
        //    GetComponent<Animator>().SetBool("IsWalking", true);
        //}
        //else
        //{
        //    GetComponent<Animator>().SetBool("IsWalking", false);
        //}
        //SetitJesus();
        //Debug
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    EnterDamageBlink(1f);
        //}
    }

    #region Public - Animatior

    public void SetWalkAnimation (bool isTrue)
    {
        //GetComponent<Animator>().SetBool("IsWalking", true);
        animator.SetBool("IsWalking", isTrue);
        //Debug.Log("SetWalkAnimation :" + isTrue);
    }

    public void SetJumpAnimation (bool isTrue)
    {
        animator.SetBool("IsJumping", isTrue);
        //Debug.Log("SetJumpAnimation :" + isTrue);
    }

    public void SetVelocityY (float y)
    {
        animator.SetFloat("VelY", y);
    }
    #endregion

    #region Public - non-animatior related
    public void FaceLeft (bool facingLeft)
    {
        sr.flipX = facingLeft;
    }

    public void EnterDamageBlink (float duration) => StartCoroutine(HurtBlink(duration));
    #endregion

    #region Hurt
    IEnumerator HurtBlink(float duration)
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
    #endregion
}

/*
 using System.Collections;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    const float BlinkInterval = 0.1f;
    public static bool IsJumping;

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

        if (Input.GetKey(KeyCode.Space))
        {
            IsJumping = true;
        }
        else IsJumping = false;
        

        if (moveX > 0.1f)
        {
            FaceLeft(false);
        }
        else if (moveX < -0.1f)
        {
            FaceLeft(true);
        }

        //Debug
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    EnterDamageBlink(1f);
        //}
       
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsWalking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsWalking", false);
        }

        if (IsJumping == true)
        {
            GetComponent<Animator>().SetBool("IsJumping", true);
            animator.GetFloat("VelY", )
        }
        else
        {
            GetComponent<Animator>().SetBool("IsJumping", false);
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
 */