using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Lvl1Intro : MonoBehaviour
{
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;
    public GameObject Image6;
    public GameObject Image7;

    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4; 
    public GameObject Text5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToSpawn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            LoadLevel();
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(3);

        Image1.SetActive(true);

        yield return new WaitForSeconds(3);

        Image1.SetActive(false);
        Text1.SetActive(true);

        yield return new WaitForSeconds(5);

        Text1.SetActive(false);
        Image2.SetActive(true);

        yield return new WaitForSeconds(3);

        Image2.SetActive(false);
        Text2.SetActive(true);

        yield return new WaitForSeconds(5);

        Text2.SetActive(false);
        Image3.SetActive(true);

        yield return new WaitForSeconds(1);

        Image3.SetActive(false);
        Image4.SetActive(true);

        yield return new WaitForSeconds(2);

        Image4.SetActive(false);
        Text3.SetActive(true);

        yield return new WaitForSeconds(5);

        Text3.SetActive(false);
        Image5.SetActive(true);

        yield return new WaitForSeconds(3);

        Image5.SetActive(false);
        Text4.SetActive(true);

        yield return new WaitForSeconds(5);

        Text4.SetActive(false);

        yield return new WaitForSeconds(4);

        Text5.SetActive(true);

        yield return new WaitForSeconds(5);

        Text5.SetActive(false);

        yield return new WaitForSeconds(1);

        Image6.SetActive(true);

        yield return new WaitForSeconds(3);

        Image6.SetActive(false);
        Image7.SetActive(true);

        yield return new WaitForSeconds(3);

        Image7.SetActive(false);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FreezerLevel1");
    }

    void LoadLevel ()
    {
        SceneManager.LoadScene("FreezerLevel1");
    }
}
