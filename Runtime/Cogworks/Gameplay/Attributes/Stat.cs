using System.Collections.Generic;

public class Stat
{
    public float baseValue;

    private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
    }

    public void Increase(float value)
    {
        baseValue += value;
    }

    public void Decrease(float value)
    {
        baseValue -= value;
    }

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
    }

    public float GetValue()
    {
        float finalValue = baseValue;

        foreach (StatModifier modifier in modifiers)
        {
            finalValue += modifier.value;
        }

        return finalValue;
    }

    public List<StatModifier> GetModifiers()
    {
        return modifiers;
    }

}
