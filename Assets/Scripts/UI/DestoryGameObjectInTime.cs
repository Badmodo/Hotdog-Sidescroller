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

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(TimeBeforeKill);
        Destroy(KillIt);
        yield return new WaitForSeconds(1.5f);
        //LoadNext.SetActive(true);
        SceneManager.LoadScene("Main Menu");
    }
}
