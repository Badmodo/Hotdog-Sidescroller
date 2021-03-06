using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    public GameObject JumpTutorial;
    public GameObject MovingWalkwaysTutorial;
    public GameObject TacticalJumpTutorial;
    public GameObject ScoreSystemTutorial;

    bool triggered_jump = false;
    bool triggered_movingWalkway = false;
    bool triggered_tacticalJump = false;
    bool triggered_ScoreSystem = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered_jump && other.gameObject.tag == "JumpEnemies")
        {
            triggered_jump = true;
            StartCoroutine(OpenTutorialWindow(JumpTutorial));
        }

        if (!triggered_movingWalkway && other.gameObject.tag == "MovingWalkways")
        {
            triggered_movingWalkway = true;
            StartCoroutine(OpenTutorialWindow(MovingWalkwaysTutorial));
        }

        if (!triggered_tacticalJump && other.gameObject.tag == "TacticalJump")
        {
            triggered_tacticalJump = true;
            StartCoroutine(OpenTutorialWindow(TacticalJumpTutorial));
        }

        if (!triggered_ScoreSystem && other.gameObject.tag == "ScoreSystem")
        {
            triggered_ScoreSystem = true;
            StartCoroutine(OpenTutorialWindow(ScoreSystemTutorial));
        }
    }

    IEnumerator OpenTutorialWindow(GameObject tutorialWindow)
    {
        tutorialWindow.SetActive(true);
        Time.timeScale = 0f;

        bool waitForKey = true;
        while (waitForKey)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape) ||
                Input.GetKeyDown(KeyCode.J))
            {
                waitForKey = false;
            }
            yield return null;
        }

        tutorialWindow.SetActive(false);
        Time.timeScale = 1f;
    }
}