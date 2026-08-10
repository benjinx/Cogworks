using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public void Awake()
    {
        DisableMouse();
    }

    public void EnableMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void DisableMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}