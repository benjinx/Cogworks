using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class StatManager : MonoBehaviour
{
    public enum StatType
    {
        // Primary Stat
        Strength, // Increase melee damage, or stamina
        Dexterity, // Improve critical hit chance, dodge rate, accuracy
        Intellect, // Increase mana pool, spell damage, magic defense
        Luck,
        
        // Secondary Stat
        AllStats,
        MaxHealth,
        MaxMana,
        
        // Offensive Stats
        Damage,
        AttackSpeed,
        CriticalChance,
        CriticalDamage,
        // Defensive Stats
        Armor,
        DodgeRate,
        Resistances,
        // Utility Stats
        MovementSpeed,
        CooldownReduction,
    }
    
    public SerializedDictionary<StatType, Stat> stats = new SerializedDictionary<StatType, Stat>();
    
    public StatManager()
    {
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            stats[type] = new Stat(type);
        }
    }

    private void Update()
    {
        UpdateTemporaryModifiers(Time.deltaTime);
    }

    public void AddStatModifier(
        string modifierName,
        string description,
        StatType statType,
        float value,
        StatModifier.StatModifierType statModifierType,
        object source,
        bool temporary = false,
        float duration = 0.0f)
    {
        if (!stats.ContainsKey(statType))
        {
            return;
        }
        
        StatModifier statModifier = new StatModifier(
            modifierName,
            description,
            statType,
            value,
            statModifierType,
            source,
            temporary,
            duration);
        
        stats[statType].AddModifier(statModifier);
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        if (!stats.ContainsKey(statModifier.statType))
        {
            return;
        }
        
        stats[statModifier.statType].AddModifier(statModifier);
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
                        
                        statModifier.onExpire?.Invoke();
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
