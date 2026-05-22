using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UI/Input Icon Set")]
public class InputIconSet : ScriptableObject
{
    [Header("Foregrounds")]
    public Texture2D northForeground;
    public Texture2D southForeground;
    public Texture2D eastForeground;
    public Texture2D westForeground;
    
    [Header("Backgrounds")]
    public Texture2D northBackground;
    public Texture2D southBackground;
    public Texture2D eastBackground;
    public Texture2D westBackground;
}