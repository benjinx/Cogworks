using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class DeviceManager : MonoBehaviour
{
    [HideInInspector]
    public InputDevice lastUsedDevice;
    
    public event Action InputDeviceChanged;
    
    private void Awake()
    {
        if (!SingletonManager.CreateSingleton(this))
        {
            return;
        }
        
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }
    
    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        // We only want state events or delta state events
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
        {
            return;
        }

        // We don't need to care if this is the same input device
        if (lastUsedDevice == device)
        {
            return;
        }

        lastUsedDevice = device;
        
        InputDeviceChanged?.Invoke();
    }
}
