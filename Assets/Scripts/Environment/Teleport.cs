using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject sp1, sp2;

    private void Start()
    {
        sp1 = this.gameObject;
    }

    void OnTriggerStay2D(Collider2D trig)
    {
        Debug.Log("Checking");
        if (Input.GetButtonDown("Vertical") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            trig.gameObject.transform.position = sp2.gameObject.transform.position;
        }
    }
}