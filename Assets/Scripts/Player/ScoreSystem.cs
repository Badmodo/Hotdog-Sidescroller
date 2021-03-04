using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreSystem : MonoBehaviour
{
    //creating a serizlied incripted save file, getting our score and sending to save file and writing to a file that will stay there through updates.
    public float timeLeft = 120;
    public static int score = 0;

    UIManager uiManager;

    public void AddScore(int amount)
    {
        score += amount;
        uiManager.SetScore(score);
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        uiManager.SetScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        uiManager.SetTime((int)timeLeft);
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.gameObject.tag == "Coin")
        {
            AddScore(10);
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "Enemy")
        {
            AddScore(100);
            Destroy(trig.gameObject);
        }
        //if (trig.gameObject.tag == "ItemBox")
        //{
        //    AddScore(50);
        //    Destroy(trig.gameObject);
        //}
    }


    void CountScore()
    {
        score = score + (int)(timeLeft * 10);
        DataManagement.datamanagement.highScore = score = (int)(timeLeft * 10);
        DataManagement.datamanagement.SaveData();
    }
}