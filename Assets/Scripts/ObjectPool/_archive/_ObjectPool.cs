//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectPool : MonoBehaviour, IPoolable
//{
//    public List<GameObject> pooledObjects;
//    public GameObject objectToPool;
//    public int amountToPool;

//    public static ObjectPool SharedInstance;

//    void Awake()
//    {
//        SharedInstance = this;
//    }

//    public GameObject GetPooledObject()
//    {
//        // The for loop is used to iterate through the ObjectPool
//        for (int i = 0; i < pooledObjects.Count; i++)
//        {
//            // Check to see if the item is active, if it is move onto the next Object.
//            // if no you exit the method and call GetPooledObject.
//            if (!pooledObjects[i].activeInHierarchy)
//            {
//                return pooledObjects[i];
//            }
//        }
//        // No inactive objects, return nothing   
//        return null;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        pooledObjects = new List<GameObject>();
//        for (int i = 0; i < amountToPool; i++)
//        {
//            GameObject obj = (GameObject)Instantiate(objectToPool);
//            obj.SetActive(false);
//            pooledObjects.Add(obj);
//        }


//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    #region Object pool
//    ObjectPoolTypeB pool;

//    public void Despawn()
//    {
//        pool.ReturnToPool(gameObject);
//    }

//    public void InitialSpawn(ObjectPoolTypeB pool)
//    {
//        this.pool = pool;
//    }
//    #endregion
//}

////GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(); 
////if (bullet != null)
////{
////    bullet.transform.position = turret.transform.position;
////    bullet.transform.rotation = turret.transform.rotation;
////    bullet.SetActive(true);
////}