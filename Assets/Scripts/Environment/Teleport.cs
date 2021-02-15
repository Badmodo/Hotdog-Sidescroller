using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject sp1, sp2;

    bool playerOverlappingDoor;
    GameObject player;

    private void Start()
    {
        sp1 = this.gameObject;
    }

    IEnumerator DetectKey ()
    {
        playerOverlappingDoor = true;
        while (playerOverlappingDoor)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                player.transform.position = sp2.gameObject.transform.position;
                playerOverlappingDoor = false;
            }
            yield return null;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameLayers.IsTargetOnPlayerLayer(collision.gameObject))
        {
            player = collision.gameObject;
            StartCoroutine(DetectKey());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (GameLayers.IsTargetOnPlayerLayer(collision.gameObject))
        {
            playerOverlappingDoor = false;
        }
    }

}