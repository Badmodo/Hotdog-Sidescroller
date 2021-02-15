using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private const int MaxHealth = 3; //A const type ensure this variable can never by modified.
    private int health;

    UIManager uiManager;


    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            uiManager.SetHealth(health);
        }
    }

    void Awake()
    {
        //When game starts, set the player's health to max health.
        health = MaxHealth;

        //Reference UIManager
        uiManager = UIManager.Instance;
    }

    void Update()
    {
        if (gameObject.transform.position.y < -6)
        {
            Die ();
        }
    }

    void Die ()
    {
        SceneManager.LoadScene("Prototype 1");
    }
}
