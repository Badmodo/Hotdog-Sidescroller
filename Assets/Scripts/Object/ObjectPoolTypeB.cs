using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPoolTypeB
{
    GameObject pf;
    Transform parent;

    List<GameObject> actives = new List<GameObject>();
    List<GameObject> pool = new List<GameObject>();

    public ObjectPoolTypeB(GameObject pf, Transform parent)
    {
        this.pf = pf;
        this.parent = parent;
    }

    public GameObject Spawn (Vector3 spawnPos, Quaternion rotation)
    {
        GameObject go;
        if (pool.Count > 0)
        {
            //Get from pool
            go = pool[0];
            go.SetActive(true);
            pool.RemoveAt(0);
            actives.Add(go);
            
        }
        else
        {
            //Create new
            go = MonoBehaviour.Instantiate(pf, spawnPos, rotation, parent);
            go.transform.position = spawnPos;
            go.GetComponent<IPoolable>().InitialSpawn(this);
            actives.Add(go);
        }
        return go;
    }



    public void ReturnToPool (GameObject go)
    {
        for (int i = 0; i < actives.Count; i++)
        {
            if (go == actives[i])
            {
                pool.Add(actives[i]);
                actives.RemoveAt(i);
                go.SetActive(false);
                return;
            }
        }
        Debug.Log("error");
    }
}

