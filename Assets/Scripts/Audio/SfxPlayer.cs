using UnityEngine;
using System.Collections;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer instance;

    [SerializeField] GameObject sfx_EnemyHurt;
    [SerializeField] GameObject sfx_PlayerWalk;
    [SerializeField] GameObject sfx_PlayerJump;
    [SerializeField] GameObject sfx_PlayerHurt;
    [SerializeField] GameObject sfx_GameWon;

    public void Play_EnemyHurt()    => SpawnSfxPrefab(sfx_EnemyHurt);
    public void Play_PlayerWalk()   => SpawnSfxPrefab(sfx_PlayerWalk);
    public void Play_PlayerJump()   => SpawnSfxPrefab(sfx_PlayerJump);
    public void Play_PlayerHurt()   => SpawnSfxPrefab(sfx_PlayerHurt);
    public void Play_GameWon()      => SpawnSfxPrefab(sfx_GameWon);

    private void Awake()
    {
        instance = this;
    }
    private void SpawnSfxPrefab(GameObject pf)
    {
        Instantiate(pf, Vector3.zero, Quaternion.identity);
    }
}