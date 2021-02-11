using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    //creating a serizlied incripted save file, getting our score and sending to save file and writing to a file that will stay there through updates.
    public float timeLeft = 120;
    public int score = 0;

    UIManager uiManager;

    public void AddScore (int amount)
    {
        score += amount;
        uiManager.SetScore(score);
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        uiManager.SetScore(score);
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        uiManager.SetTime((int)timeLeft);
        //timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left:" + (int)timeLeft);
        //scoreUI.gameObject.GetComponent<Text>().text = ("Score:" + score);
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
            CountScore();
            //saves data
            DataManagement.datamanagement.SaveData();
        }
        if (trig.gameObject.tag == "Coin")
        {
            AddScore(10);
            Destroy(trig.gameObject);
        }
    }

    void CountScore ()
    {
        score = score + (int)(timeLeft * 10);
        DataManagement.datamanagement.highScore = score = (int)(timeLeft * 10);
        DataManagement.datamanagement.SaveData();
    }
}
