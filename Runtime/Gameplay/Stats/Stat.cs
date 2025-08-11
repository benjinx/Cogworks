using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class Stat
{
    public StatManager.StatType type;
    
    // The original base value, we should never change this from the original
    // Starting Stat value, if a secondary, it will be calculated instead of set
    public float baseValue;

    // Permanent value such as talent tree or attributes from a level-up
    // Level ups, talents, permanent effects
    private float permanentValue;
    
    public UnityEvent<Stat> onStatChange = new UnityEvent<Stat>();

    [SerializeField]
    private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(StatManager.StatType type)
    {
        this.type = type;
        this.baseValue = 0;
    }
    
    public Stat(StatManager.StatType type, float baseValue)
    {
        this.type = type;
        this.baseValue = baseValue;
        
        onStatChange?.Invoke(this);
    }

    public void IncreasePermanent(float value)
    {
        permanentValue += value;
        
        onStatChange?.Invoke(this);
    }

    public void DecreasePermanent(float value)
    {
        permanentValue -= value;
        
        onStatChange?.Invoke(this);
    }
    
    public void IncreaseByPercentage(int value)
    {
        // permanentValue *= (1 + percent / 100.0f);
        permanentValue += permanentValue * (value / 100.0f);
        
        onStatChange?.Invoke(this);
    }
    
    public void DecreaseByPercentage(int value)
    {
        // permanentValue *= (1 - percent / 100.0f);
        permanentValue -= permanentValue * (value / 100.0f);
        
        onStatChange?.Invoke(this);
    }
    
    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        
        onStatChange?.Invoke(this);
    }

    public void AddModifiers(List<StatModifier> modifiers)
    {
        this.modifiers.AddRange(modifiers);
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
        
        onStatChange?.Invoke(this);
    }

    public void RemoveAllModifiersFromSource(object source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        
        onStatChange?.Invoke(this);
    }

    public float GetValue()
    {
        float flatAdd = 0.0f;
        float percentAdd = 0.0f;
        float percentMult = 1.0f;
        
        foreach (StatModifier modifier in modifiers)
        {
            switch (modifier.statModifierType)
            {
                case StatModifier.StatModifierType.Flat:
                    flatAdd += modifier.value;
                    break;
                case StatModifier.StatModifierType.PercentAdditive:
                    percentAdd += modifier.value;
                    break;
                case StatModifier.StatModifierType.PercentMultiplicative:
                    percentMult *= 1 + modifier.value / 100.0f;
                    break;
            }
        }

        return (baseValue + permanentValue + flatAdd) * (1.0f + percentAdd / 100.0f) * percentMult;
    }

    public List<StatModifier> GetModifiers()
    {
        return modifiers;
    }
}
