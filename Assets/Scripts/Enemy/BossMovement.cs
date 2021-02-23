using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    public float speed = 10;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}

