using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestoryGameObjectInTime : MonoBehaviour
{

    public GameObject KillIt;
    public float TimeBeforeKill;
   // public GameObject LoadNext;

    void Start()
    {
        StartCoroutine(DestroyGameObject());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(TimeBeforeKill);
        Destroy(KillIt);
        yield return new WaitForSeconds(1.5f);
        //LoadNext.SetActive(true);
        SceneManager.LoadScene("Main Menu");
    }
}
