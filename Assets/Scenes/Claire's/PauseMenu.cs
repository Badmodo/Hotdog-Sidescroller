using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPrefab;
    private bool isInstantiated = false;
    //private bool isPaused = false;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInstantiated && pauseMenuPrefab == null) throw new System.Exception("Something weird is happening.");
            if (!isInstantiated)
            {
                Instantiate(pauseMenuPrefab);
                isInstantiated = true;
                TogglePause(true);
            }
          //  if (isInstantiated) TogglePause(false);
        }
    }
    private void Paused()
    {
        Time.timeScale = 0;
        pauseMenuPrefab.SetActive(true);
    }
    private void UnPaused()
    {
        Time.timeScale = 1;
        pauseMenuPrefab.SetActive(false);
    }
    public void TogglePause(bool _toggle)
    {
        _toggle = !_toggle;
        if (_toggle) Paused();
        else UnPaused();
    }
}