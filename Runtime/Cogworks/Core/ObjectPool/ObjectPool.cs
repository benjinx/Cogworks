using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    public GameObject objectToPool;

    public int startPoolSize = 10;

    public int maxPoolSize = 10;

    // Collection checks will throw errors if we try to release an item that is already in the pool
    public bool collectionCheck = true;

    // So due to how the unity object pool works, it doesn't preheat the objects,
    // if we'd like to i'll allow it, but i wouldn't recommend it
    public bool shouldPreheat = false;

    public IObjectPool<GameObject> pool;


    void Awake()
    {
        if (poolType == PoolType.Stack)
        {
            pool = new ObjectPool<GameObject>(CreatePooledObject,
              OnGetFromPool,
              OnReturnToPool,
              OnDestroyPooledObject,
              collectionCheck,
              startPoolSize,
              maxPoolSize);
        }
        else if (poolType == PoolType.LinkedList)
        {
            pool = new LinkedPool<GameObject>(CreatePooledObject,
                      OnGetFromPool,
                      OnReturnToPool,
                      OnDestroyPooledObject,
                      collectionCheck,
                      maxPoolSize);
        }

        if (shouldPreheat)
        {
            Preheat();
        }
    }

    private GameObject CreatePooledObject()
    {
        GameObject gobj = Instantiate(objectToPool);

        gobj.AddComponent<PooledObject>();

        return gobj;
    }

    private void OnGetFromPool(GameObject gobj)
    {
        gobj.SetActive(true);

        PooledObject pooledObject = gobj.GetComponent<PooledObject>();

        pooledObject.OnSpawned();
    }

    private void OnReturnToPool(GameObject gobj)
    {
        gobj.SetActive(false);

        PooledObject pooledObject = gobj.GetComponent<PooledObject>();

        pooledObject.OnDespawned();
    }

    private void OnDestroyPooledObject(GameObject gobj)
    {
        Destroy(gobj);
    }

    private void Preheat()
    {
        List<GameObject> tmpPreheatList = new List<GameObject>();

        for (int i = 0; i < startPoolSize; ++i)
        {
            // Create the amount we're looking for.
            GameObject gobj = pool.Get();

            // Set parent to this pool object
            gobj.transform.parent = transform;

            // Add to the preheat list so we can release after
            tmpPreheatList.Add(gobj);
        }

        // Release all now that they're been created
        for (int i = 0; i < tmpPreheatList.Count; ++i)
        {
            pool.Release(tmpPreheatList[i]);
        }
    }
}
