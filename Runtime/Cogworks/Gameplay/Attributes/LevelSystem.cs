using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelSystem : MonoBehaviour
{
    public int level = 1;

    public float experience;

    public float experienceToNextLevel => CalculateXPForLevel(level + 1);

    public UnityEvent onLevelUp;

    public Action onLevelUp_Internal;

    public void GainExperience(float amount)
    {
        experience += amount;

        while (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        onLevelUp?.Invoke();
        onLevelUp_Internal?.Invoke();
    }

    // Runescape-like XP formula
    private int CalculateXPForLevel(int level)
    {
        return Mathf.FloorToInt((50 * Mathf.Pow(level, 2) - 50 * level) / 2);
    }

    // Oldschool Runescape-like XP formula, for fun
    private const float EulerMascheroni = 0.5772156649f; // y (Euler-Mascheroni constant)

    private int CalculateXPForLevel2(int level)
    {
        if (level <= 1) return 0; // Level 1 requires no xp

        float powerFactor = Mathf.Pow(2, level / 7.0f);
        float xp = (1.0f / 8.0f) * (
            (level * level) - level + 600 * ((powerFactor - (21.0f / 7.0f)) / (Mathf.Pow(2, 1.0f / 7.0f) - 1.0f))
            - (level / 10.0f) - EulerMascheroni
            );

        return Mathf.FloorToInt(xp);
    }
}
