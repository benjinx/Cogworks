using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;

public class DeviceSpecificDisplayUIToolkit : MonoBehaviour
{
    public enum DeviceType
    {
        None,
        PC, // UNITY_EDITOR, UNITY_STANDALONE
        Xbox, // UNITY_XBOXONE, UNITY_GAMECORE_XBOX
        Playstation, // UNITY_PS4, UNITY_PS5
        Switch // UNITY_SWITCH
    }
    
    [System.Serializable]
    public class DisplayData
    {
        public DeviceType deviceType;

        public InputIconSet objectsToDisplay = null;
    }
    
    public List<DisplayData> deviceSpecificData = new List<DisplayData>();
    
    private DeviceType currentDeviceType = DeviceType.None;
    
    public void Start()
    {
        DeviceManager deviceManager = SingletonManager.Get<DeviceManager>();
        
        deviceManager.InputDeviceChanged += InputDeviceChanged;
        
        if (deviceManager.lastUsedDevice != null)
        {
            InputDeviceChanged();
        }
    }

    private void OnDisable()
    {
        SingletonManager.Get<DeviceManager>().InputDeviceChanged -= InputDeviceChanged;
    }

    private void InputDeviceChanged()
    {
        // Detect input device type and update
        if (SingletonManager.Get<DeviceManager>().lastUsedDevice is Gamepad gamepad)
        {
            if (gamepad is XInputController)
            {
                currentDeviceType = DeviceType.Xbox;
            }
            else if (gamepad is DualSenseGamepadHID || gamepad is DualShockGamepad)
            {
                currentDeviceType = DeviceType.Playstation;
            }
            else if (gamepad is SwitchProControllerHID)
            {
                currentDeviceType = DeviceType.Switch;
            }
        }
        else if (SingletonManager.Get<DeviceManager>().lastUsedDevice is Keyboard ||
                 SingletonManager.Get<DeviceManager>().lastUsedDevice is Mouse)
        {
            currentDeviceType = DeviceType.PC;
        }
        
        // Default to pc if not found
        if (currentDeviceType == DeviceType.None)
        {
            currentDeviceType = DeviceType.PC;
        }
        
        // Cache the group of objects to display based on the device type
        DisplayData dataToDisplay = deviceSpecificData.Find(d => d.deviceType == currentDeviceType);

        // Default to something if we don't find one
        if (dataToDisplay == null)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            dataToDisplay = deviceSpecificData.Find(d => d.deviceType == DeviceType.PC);
#endif

#if UNITY_XBOXONE || UNITY_GAMECORE_XBOX
            dataToDisplay = deviceSpecificData.Find(d => d.deviceType == DeviceType.Xbox);
#endif

#if UNITY_PS4 || UNITY_PS5
            dataToDisplay = deviceSpecificData.Find(d => d.deviceType == DeviceType.Playstation);
#endif

#if UNITY_SWITCH
            dataToDisplay = deviceSpecificData.Find(d => d.deviceType == DeviceType.Switch);
#endif
        }

        // If no default found, end
        if (dataToDisplay == null)
        {
            return;
        }
        
        // Change them all
        //SingletonManager.Get<HUDUI>().ChangeIconTexture(dataToDisplay.objectsToDisplay);
    }
}
