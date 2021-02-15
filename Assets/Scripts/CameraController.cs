using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
        }
    }
}
