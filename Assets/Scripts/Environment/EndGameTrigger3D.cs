using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger3D : MonoBehaviour
{
    UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameLayers.IsTargetOnPlayerLayer(other.gameObject))
        {
            uiManager.OpenScoreBoard(PlayerController3D.Score);
        }
    }
}