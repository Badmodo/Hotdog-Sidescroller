using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using EditorApplication = UnityEditor.EditorApplication;



[RequireComponent(typeof(ScoreBoardController))]
[RequireComponent(typeof(HUD))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject pauseMenu;
    public bool PauseMenuOpen;
    public GameObject LevelTitle;

    ScoreBoardController scoreBoard;
    HUD hud;

    private void Start()
    {
        //Starts the title screen
        StartCoroutine(TitleActivator());
    }

    IEnumerator TitleActivator()
    {
        yield return new WaitForSeconds(.5f);
        if (LevelTitle != null)
        {
            LevelTitle.SetActive(true);
            Time.timeScale = 0;
        }        
    }

    void Awake()
    {
        Instance = this;
        scoreBoard = GetComponent<ScoreBoardController>();
        hud = GetComponent<HUD>();
    }

    public void OpenScoreBoard(int score)
    {
        scoreBoard.OpenScoreBoard(score);
    }

    #region HUD
    public void SetHealth(int amount) => hud.SetHealth(amount);
    public void SetTime(int amount) => hud.SetTime(amount);
    public void SetScore(int amount) => hud.SetScore(amount);
    #endregion

    void Update ()
    {
        //open and close the pause menu
        if (PauseMenuOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                PauseMenuOpen = true;
            }
        }

        else if (PauseMenuOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                PauseMenuOpen = false;
            }
        }

        //used to close the title screen
        if (Input.GetKeyDown("e"))
        {
            if (LevelTitle != null)
            {
                LevelTitle.SetActive(false);
            }
            Time.timeScale = 1;
        }
    }

    #region PauseMenu
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        PauseMenuOpen = false;
    }
    #endregion
}