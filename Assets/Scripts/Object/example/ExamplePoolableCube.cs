using System.Collections;
using UnityEngine;

//All poolable objects need to implement IPoolable
public class ExamplePoolableCube : MonoBehaviour, IPoolable
{
    ObjectPoolTypeB pool;

    public void Despawn()
    {
        Debug.Log("a");
        pool.ReturnToPool(gameObject);
    }

    public void InitialSpawn(ObjectPoolTypeB pool)
    {
        this.pool = pool;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) //RMB
        {
            Despawn();
        }
    }
}