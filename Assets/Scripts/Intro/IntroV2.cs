using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroV2 : MonoBehaviour
{
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;

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

    // Update is called once per frame
    void Update()
    {
 
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(Image1);

        Instantiate(Text1);
    }
}
