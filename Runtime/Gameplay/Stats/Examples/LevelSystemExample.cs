using UnityEngine;

[RequireComponent(typeof(LevelSystem))]
public class LevelSystemExample : MonoBehaviour
{
    private LevelSystem levelSystem;
    
    void Awake()
    {
        levelSystem = GetComponent<LevelSystem>();

        levelSystem.onLevelUp_Internal += LevelUp;

        levelSystem.GainExperience(500);
    }

    public void LevelUp()
    {
        Debug.Log("Leveled up to: " + levelSystem.level);
    }
}
