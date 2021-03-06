using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUIRotation : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
