using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPoolTypeB : MonoBehaviour
{
    public static ObjectPoolTypeB Instance;

    public GameObject pf;

    List<GameObject> actives = new List<GameObject>();
    List<GameObject> pool = new List<GameObject>();


    void Awake()
    {
        Instance = this;
    }

    public GameObject Spawn (Vector3 spawnPos)
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
            go = Instantiate(pf, spawnPos, Quaternion.identity, transform);
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

//GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(); 
//if (bullet != null)
//{
//    bullet.transform.position = turret.transform.position;
//    bullet.transform.rotation = turret.transform.rotation;
//    bullet.SetActive(true);
//}