using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] players;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt ("SelectedCharacter") == 0)
        {
            Instantiate(players[(0)], Vector2.zero, Quaternion.identity);
        }
        if (PlayerPrefs.GetInt("SelectedCharacter") == 1)
        {
            Instantiate(players[(1)], Vector2.zero, Quaternion.identity);
        }
    }
}
