using System.Collections;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float WaitBeforeDestroy = 0.5f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(WaitBeforeDestroy);
        Destroy(gameObject);
    }

}