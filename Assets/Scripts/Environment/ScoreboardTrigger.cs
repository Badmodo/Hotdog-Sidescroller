using UnityEngine;
using System.Collections;

public class ScoreboardTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        UIManager.Instance?.OpenScoreBoard();
    }
}