using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

    Image healthBar;
    float maxHealth = 100f;
    public static float health;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHealth;
        StartCoroutine(WaitToSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;    
    }
    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(5);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
        BossHealthBar.health -= 10f;
        yield return new WaitForSeconds(0.5f);
    }
}


