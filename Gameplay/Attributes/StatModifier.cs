using UnityEngine;

public class StatModifier
{
    public float value;

    public bool isTemporary;

    public float duration;

    public StatModifier(float value, bool isTemporary, float duration)
    {
        this.value = value;
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
}
