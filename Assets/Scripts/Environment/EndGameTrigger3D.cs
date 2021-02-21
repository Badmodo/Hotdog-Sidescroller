using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger3D : MonoBehaviour
{
    bool triggered = false;
    
    UIManager uiManager;


    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && GameLayers.IsTargetOnPlayerLayer(other.gameObject))
        {
            triggered = true;
            SfxPlayer.instance.Play_GameWon();

            uiManager.OpenScoreBoard(PlayerController3D.FinalScore);
        }
    }
}