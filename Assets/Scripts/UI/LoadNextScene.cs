using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNextScene : MonoBehaviour
{
    public GameObject ScoreScreen;
    public int nextLevel;

    public void GoToNextLevel()
    {
        // LevelLoader.LoadGameplayLevel();
        SceneManager.LoadScene(nextLevel);
    }

    private void Awake()
    {
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void Update()
    {
        //if score is open use space to move to next screen
        if (ScoreScreen == true)
        {
            if (Input.GetKeyDown("e"))
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
