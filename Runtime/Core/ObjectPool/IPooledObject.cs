using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    public UnityEvent onSpawned;

    public UnityEvent onDespawned;

    public void OnSpawned()
    {
        onSpawned?.Invoke();
    }

    public void OnDespawned()
    {
        onDespawned?.Invoke();
    }
}
