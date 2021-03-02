using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
{
    protected ObjectPoolManager poolManager;

    protected virtual void Start ()
    {
        poolManager = ObjectPoolManager.Instance;
    }
}