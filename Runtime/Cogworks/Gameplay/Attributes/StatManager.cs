using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

    private void Update()
    {
        UpdateTemporaryModifiers(Time.deltaTime);
    }

    public void ModifyStat(string statName, float value, bool temporary = false, float duration = 0.0f)
    {
        if (!stats.ContainsKey(statName))
        {
            return;
        }

        StatModifier statModifier = new StatModifier(value, temporary, duration);
        stats[statName].AddModifier(statModifier);
    }

    private void UpdateTemporaryModifiers(float deltaTime)
    {
        foreach (Stat stat in stats.Values)
        {
            List<StatModifier> expiredModifiers = new List<StatModifier>();

            foreach (StatModifier statModifier in stat.GetModifiers())
            {
                if (statModifier.isTemporary)
                {
                    statModifier.ReduceDuration(deltaTime);

                    if (statModifier.duration <= 0.0f)
                    {
                        expiredModifiers.Add(statModifier);
                    }
                }
            }

            foreach (StatModifier statModifier in expiredModifiers)
            {
                stat.RemoveModifier(statModifier);
            }
        }
    }
}
