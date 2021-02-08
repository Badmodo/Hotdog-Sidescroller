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
    public GameObject timeLeftUI;
    public GameObject scoreUI;

    private void Start()
    {
        timeLeftUI = GameObject.Find("TimeLeft");
        scoreUI = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left:" + (int)timeLeft);
        scoreUI.gameObject.GetComponent<Text>().text = ("Score:" + score);
        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene("Prototype 1");
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
            score += 10;
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
