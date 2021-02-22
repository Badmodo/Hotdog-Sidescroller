using System.Collections;
using UnityEngine;

public class CameraController3D : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    [SerializeField] //PlayerController3D player;
    GameObject player;
    float startingZ;

    void Start()
    {
        startingZ = transform.position.z;
        //player = (PlayerController3D)FindObjectOfType(typeof(PlayerController3D));
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
            float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(x, y, startingZ),
                10f * Time.deltaTime);
        }
    }
}
