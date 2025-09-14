using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // public enum PoolType
    // {
    //     Stack, // FILO, push/pop as a normal stack, most common
    //     LinkedList // Used for lots of temporary pooled objects created and released quickly
    // }

    // public PoolType poolType;

    public GameObject objectToPool;

    // So due to how the unity object pool works, it doesn't preheat the objects, but we will if
    // this number is greater than 0
    public int startPoolSize = 0;

    public int maxPoolSize = 10;

    private bool collectionCheck = false;

    // Used in preheat, allows us to not fire off onspawn/ondespawn for it.
    private bool suppressEvents = false;

    public ObjectPool<GameObject> pool;

    void Awake()
    {
        pool = new ObjectPool<GameObject>(
            CreatePooledObject,
            OnGetFromPool,
            OnReturnToPool,
            OnDestroyPooledObject,
            collectionCheck,
            startPoolSize,
            maxPoolSize);

        if (startPoolSize > 0)
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
            gobj.GetComponent<PooledObject>()?.OnSpawn();
        }
    }

    private void OnReturnToPool(GameObject gobj)
    {
        if (!suppressEvents)
        {
            gobj.GetComponent<PooledObject>()?.OnDespawn();
        }

        gobj.SetActive(false);

        gobj.transform.SetParent(transform);
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
        for (int i = 0; i < startPoolSize; ++i)
        {
            tmp.Add(pool.Get());
        }

        // Release all now that they've been created
        for (int i = 0; i < tmp.Count; ++i)
        {
            pool.Release(tmp[i]);
        }

        suppressEvents = false;
    }
}
