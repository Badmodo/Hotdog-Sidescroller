using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    const string LevelName_Gameplay = "Prototype 1";

    public static void LoadMainMenu ()
    {

    }

    public static void LoadGameplayLevel ()
    {
        SceneManager.LoadScene(LevelName_Gameplay);
    }
}