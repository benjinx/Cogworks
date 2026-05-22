using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    private static Dictionary<System.Type, MonoBehaviour> singletons = new Dictionary<System.Type, MonoBehaviour>();
    
    public static bool CreateSingleton<T> (T monoBehaviour) where T : MonoBehaviour
    {
        var type = typeof(T);

        if (singletons.TryGetValue(type, out var existing))
        {
            if (existing)
            {
                Debug.LogWarning("Singleton already exists. " + type +
                                 " here: " + monoBehaviour.gameObject.name +
                                 " and here: " + existing.gameObject.name);
                
                Destroy(monoBehaviour);
            
                return false;
            }
            
            singletons.Remove(type); // cleanup dead reference
        }
        
        singletons[typeof(T)] = monoBehaviour;
        
        // DontDestroyOnLoad(monoBehaviour.gameObject); // Optional if we don't want to add ours...
        // monoBehaviour.gameObject.AddComponent<DontDestroyOnLoad>(); // Same thing
        
        return true;
    }

    public static T Get<T> () where T : MonoBehaviour
    {
        var type = typeof(T);

        if (singletons.TryGetValue(type, out var instance))
        {
            if (instance)
            {
                return (T)instance;
            }
            
            singletons.Remove(type);
        }

        return null;
    }
}
