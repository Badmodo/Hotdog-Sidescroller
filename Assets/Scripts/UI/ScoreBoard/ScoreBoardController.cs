﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] GameObject scoreScreenGroup;
    [SerializeField] Text highscoreText;

    float scoreTickDurtion = 2f;
    public int nextLevel;

    public void GoToNextLevel ()
    {
        // LevelLoader.LoadGameplayLevel();
        SceneManager.LoadScene(nextLevel);
    }

    public void OpenScoreBoard(int score)
    {
        scoreScreenGroup.SetActive(true);

        //int score = 9999;
        //ScoreSystem scoreSystem = (ScoreSystem)FindObjectOfType(typeof(ScoreSystem));
        //if (scoreSystem != null)
        //    score = scoreSystem.score;

        StartCoroutine(PlayScoreTickingAnimation(score));
    }

    public void CloseScoreBoard()
    {
        scoreScreenGroup.SetActive(false);
    }

    void Awake()
    {
        CloseScoreBoard();
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GoToNextLevel();
        }
    }

    IEnumerator PlayScoreTickingAnimation(int highScore)
    {
        //Tick and increase the highscore text from 0 to the current score
        float score = 0;
        float t = 0;

        while (t < scoreTickDurtion)
        {
            t += Time.deltaTime;
            float t2 = t / scoreTickDurtion;
            score = Mathf.Lerp(0, highScore, t2);
            highscoreText.text = $"{Mathf.CeilToInt(score):000000}";
            yield return null;
        }

        highscoreText.text = $"{(int)highScore:000000}";
    }
}