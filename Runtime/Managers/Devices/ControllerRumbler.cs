using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumbler : MonoBehaviour
{
    private Coroutine rumbleCoroutine;
    
    public void StartRumble(float lowFrequency, float highFrequency)
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }
    
    public void StartRumble(float lowFrequency, float highFrequency, float duration)
    {
        if (rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
        }
        
        rumbleCoroutine = StartCoroutine(RumbleCoroutine(lowFrequency, highFrequency, duration));
    }

    private IEnumerator RumbleCoroutine(float lowFrequency, float highFrequency, float duration)
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
            
            yield return new WaitForSeconds(duration);

            Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        }

        rumbleCoroutine = null;
    }

    public void StopRumble()
    {
        if (rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
            rumbleCoroutine = null;
        }
        
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        }
    }
}