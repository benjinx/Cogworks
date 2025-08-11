using System;
using UnityEngine.Events;

[Serializable]
public class StatModifier
{
    public enum StatModifierType
    {
        Flat,
        PercentAdditive,
        PercentMultiplicative,
    }
    
    public string modifierName;

    public string description;
    
    public StatManager.StatType statType;
    
    public StatModifierType statModifierType;
    
    public float value;

    public bool isTemporary;

    public float duration;

    public object source;
    
    public UnityEvent onExpire = new UnityEvent();
    
    public StatModifier(
        string modifierName,
        string description,
        StatManager.StatType statType,
        float value,
        StatModifierType statModifierType,
        object source,
        bool isTemporary = false,
        float duration = 0.0f)
    {
        this.modifierName = modifierName;
        this.description = description;
        this.statType = statType;
        this.value = value;
        this.statModifierType = statModifierType;
        this.source = source;
        this.isTemporary = isTemporary;
        this.duration = duration;
    }

    public void ReduceDuration(float amount)
    {
        if (isTemporary)
        {
            duration -= amount;
        }
    }
    
    public StatModifier CloneWithNewSource(object newSource)
    {
        return new StatModifier(
            this.modifierName,
            this.description,
            this.statType,
            this.value,
            this.statModifierType,
            newSource,
            this.isTemporary,
            this.duration
        );
    }
}
