using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

    Image healthBar;
    float maxHealth = 100f;
    public static float health;

    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
        StartCoroutine(WaitToSpawn());
    }

    void Update()
    {
        healthBar.fillAmount = health / maxHealth;    
    }
    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(4f);
        BossHealthBar.health -= 30f;

        yield return new WaitForSeconds(5f);
        BossHealthBar.health -= 30f;

        yield return new WaitForSeconds(9f);
        BossHealthBar.health -= 30f;
    }
}


