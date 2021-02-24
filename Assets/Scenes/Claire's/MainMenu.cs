using UnityEngine;
using EditorApplication = UnityEditor.EditorApplication;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
public class MainMenu : MonoBehaviour
{
    public void ChangeScene(int _sceneID) => SceneManager.LoadScene(_sceneID);
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}