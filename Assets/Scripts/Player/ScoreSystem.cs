using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreSystem : MonoBehaviour
{
    //creating a serizlied incripted save file, getting our score and sending to save file and writing to a file that will stay there through updates.
    public float timeLeft = 120;
    public int score = 0;

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
        uiManager.SetTime((int)timeLeft);
        if (timeLeft < 0.1f)
        {
            LevelLoader.LoadGameplayLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        //adds score to the score object
        if (trig.gameObject.tag == "LevelEnd")
        {
            uiManager.OpenScoreBoard();
            //CountScore();
            //saves data
            //DataManagement.datamanagement.SaveData();
        }
        if (trig.gameObject.tag == "Coin")
        {
            score += 10;
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "Enemy")
        {
            score += 100;
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "ItemBox")
        {
            score += 50;
            Destroy(trig.gameObject);
        }
    }


    //void CountScore()
    //{
    //    score = score + (int)(timeLeft * 10);
    //    DataManagement.datamanagement.highScore = score = (int)(timeLeft * 10);
    //    DataManagement.datamanagement.SaveData();
    //}
}