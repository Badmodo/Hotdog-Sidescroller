using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameLayers.IsTargetOnPlayerLayer(other.gameObject))
        {
            //other.transform.parent = transform;
            other.transform.SetParent(transform);
            other.GetComponent<PlayerController3D>().SetSteppedOnMovingPlatform(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameLayers.IsTargetOnPlayerLayer(other.gameObject))
        {
            //other.transform.parent = null;
            other.transform.SetParent(null);
            other.GetComponent<PlayerController3D>().SetSteppedOnMovingPlatform(false);
        }
    }
}
