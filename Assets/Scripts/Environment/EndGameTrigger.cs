using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameLayers.IsTargetOnPlayerLayer(collision.gameObject))
        {
            SfxPlayer.instance.Play_GameWon();

            uiManager.OpenScoreBoard(ScoreSystem.score);
        }
    }
}