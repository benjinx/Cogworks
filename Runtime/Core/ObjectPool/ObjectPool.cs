using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public enum PoolType
    {
        Stack, // FILO, push/pop as a normal stack, most common
        LinkedList // Used for lots of temporary pooled objects created and released quickly
    }

    public PoolType poolType;

    public GameObject objectToPool;

    public int startPoolSize = 10;

    public int maxPoolSize = 10;

    public bool collectionCheck = false;

    // So due to how the unity object pool works, it doesn't preheat the objects,
    // if we'd like to i'll allow it
    public bool shouldPreheat = false;

    // how many should we preheat
    public int preheatAmount = 0;

    // Used in preheat, allows us to not fire off onspawn/ondespawn for it.
    private bool suppressEvents = false;

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
        GameObject gobj = Instantiate(objectToPool, transform, true);

        if (!gobj.TryGetComponent<PooledObject>(out _))
        {
            gobj.AddComponent<PooledObject>();
        }

        return gobj;
    }

    private void OnGetFromPool(GameObject gobj)
    {
        gobj.SetActive(true);

        if (!suppressEvents)
        {
            gobj.GetComponent<PooledObject>()?.OnSpawned();
        }
    }

    private void OnReturnToPool(GameObject gobj)
    {
        gobj.SetActive(false);

        gobj.transform.SetParent(transform);

        if (!suppressEvents)
        {
            gobj.GetComponent<PooledObject>()?.OnDespawned();
        }
    }

    private void OnDestroyPooledObject(GameObject gobj)
    {
        Destroy(gobj);
    }

    private void Preheat()
    {
        suppressEvents = true;

        List<GameObject> tmp = new List<GameObject>();

        // Create the desired amount
        for (int i = 0; i < preheatAmount; ++i)
        {
            tmp.Add(pool.Get());
        }

        // Release all now that they're been created
        for (int i = 0; i < tmp.Count; ++i)
        {
            pool.Release(tmp[i]);
        }

        suppressEvents = false;
    }
}
