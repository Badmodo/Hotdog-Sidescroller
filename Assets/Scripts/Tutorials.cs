using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    public GameObject JumpTutorial;
    public GameObject MovingWalkways;




    void Start()
    {
            JumpTutorial.SetActive(true);
            Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)
                || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = 1;
            JumpTutorial.SetActive(false);
            MovingWalkways.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MovingWalkways")
        {
            MovingWalkways.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
