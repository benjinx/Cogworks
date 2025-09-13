using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    public UnityEvent onSpawn;

    public UnityEvent onDespawn;

    public void OnSpawn()
    {
        onSpawn?.Invoke();
    }

    public void OnDespawn()
    {
        onDespawn?.Invoke();
    }
}
