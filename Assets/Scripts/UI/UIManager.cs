using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ScoreBoardController))]
[RequireComponent(typeof(HUD))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    ScoreBoardController scoreBoard;
    HUD hud;

    void Awake()
    {
        Instance = this;
        scoreBoard = GetComponent<ScoreBoardController>();
        hud = GetComponent<HUD>();
    }

    public void OpenScoreBoard ()
    {
        scoreBoard.OpenScoreBoard();
    }

    public void SetHealth (int amount)
    {
        hud.SetHealth(amount);
    }
}