using UnityEngine;
using System.Collections;

public class CharacterRenderer : MonoBehaviour
{
    SpriteRenderer sr;

    Color colorHide = new Color(1, 1, 1, 0);

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerDamaged();
        }
    }

    public void PlayerDamaged ()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink ()
    {
        bool reveal = false;
        for (int i = 0; i < 8; i++)
        {
            sr.color = reveal ? Color.white : colorHide;
            reveal = !reveal;
            yield return new WaitForSeconds(0.1f);
        }
        sr.color = Color.white;
    }
}