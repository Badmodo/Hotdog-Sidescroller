using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    public GameObject JumpTutorial;
    public GameObject MovingWalkwaysTutorial;
    public GameObject TacticalJumpTutorial;
    public GameObject ScoreSystemTutorial;


    void Start()
    {
            JumpTutorial.SetActive(true);
            Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {            
            Time.timeScale = 1;
            JumpTutorial.SetActive(false);
            MovingWalkwaysTutorial.SetActive(false);
            TacticalJumpTutorial.SetActive(false);
            ScoreSystemTutorial.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingWalkways")
        {
            MovingWalkwaysTutorial.SetActive(true);
            Time.timeScale = 0;
        }

        if (other.gameObject.tag == "TacticalJump")
        {
            TacticalJumpTutorial.SetActive(true);
            Time.timeScale = 0;
        }

        if (other.gameObject.tag == "ScoreSystem")
        {
            ScoreSystemTutorial.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
