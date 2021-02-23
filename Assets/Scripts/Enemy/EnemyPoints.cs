using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoints : MonoBehaviour
{
    public KillManager kills_score;

    void Start()
    {
        kills_score = GameObject.FindWithTag("GameManager").GetComponent<KillManager>();
    }

    public void OnCollisionEnter(Collision collision) 
    {
        if (transform.gameObject.CompareTag("Enemy"))
        {
            kills_score.Increase_score(); //Calling the function from 'GameManager'
        }
    }
}
