using UnityEngine;
using System.Collections;

public class GameLayers : MonoBehaviour
{
    public static GameLayers Instance;

    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;
    public LayerMask EnemyLayer;

    void Awake()
    {
        Instance = this;
    }

    public static bool IsTargetOnPlayerLayer (GameObject go) => Instance.PlayerLayer == (Instance.PlayerLayer | 1 << go.layer);
    public static bool IsTargetOnEnemyLayer(GameObject go) => Instance.EnemyLayer == (Instance.EnemyLayer | 1 << go.layer);
    public static bool IsTargetOnGroundLayer(GameObject go) => Instance.GroundLayer == (Instance.GroundLayer | 1 << go.layer);
}