﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private const int MaxHealth = 3; //A const type ensure this variable can never by modified.
    private int health;

    public GameObject YouDied;

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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Boss")
        {
            Die();
        }
    }

    void Awake()
    {
        //When game starts, set the player's health to max health.
        health = MaxHealth;
    }

    void Start()
    {
        //Reference UIManager
        uiManager = UIManager.Instance;
    }

    void Update()
    {
        if (gameObject.transform.position.y < -6)
        {
            Die ();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            health = Mathf.Clamp(health + 1, 0, 3);
            uiManager.SetHealth(health);
        }
    }

    void Die ()
    {
        StartCoroutine(youDied());
    }
    
    IEnumerator youDied()
    {
        if (YouDied != null)
        {
            YouDied.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
