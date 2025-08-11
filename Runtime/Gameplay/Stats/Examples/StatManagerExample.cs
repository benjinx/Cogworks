using UnityEngine;

public class StatManagerExample : MonoBehaviour
{
    private StatManager statManager;

    private void Start()
    {
        statManager = gameObject.AddComponent<StatManager>();

        // Set initial stat values
        statManager.stats[StatManager.StatType.Strength] = new Stat(StatManager.StatType.Strength, 10);
        statManager.stats[StatManager.StatType.Dexterity] = new Stat(StatManager.StatType.Dexterity, 8);
        statManager.stats[StatManager.StatType.Intellect] = new Stat(StatManager.StatType.Intellect, 6);

        // Example of level up might automatically do this, or player chose to increase this via point distribution
        statManager.stats[StatManager.StatType.Strength].IncreasePermanent(2);

        // Equip an item that gives +5 strength
        statManager.AddStatModifier(
            "Sword of Strength",
            "Strength",
            StatManager.StatType.Strength,
            5,
            StatModifier.StatModifierType.Flat,
            this);

        // Apply a temporary +2 dexterity for 5 seconds
        statManager.AddStatModifier(
            "Cat-like Reflexes",
            "Dexterity for 5 seconds.",
            StatManager.StatType.Dexterity,
            2, 
            StatModifier.StatModifierType.Flat, 
            this,
            true, 
            5.0f);

        // Get current strength value
        float strengthValue = statManager.stats[StatManager.StatType.Strength].GetValue();
        Debug.Log("Strength Value: " + strengthValue);

        float dexterityValue = statManager.stats[StatManager.StatType.Dexterity].GetValue();
        Debug.Log("Dexterity Value: " + dexterityValue);

        Debug.Log("Please wait 5 seconds for duration...");
        Invoke(nameof(DelayedDexLog), 5.0f);
    }

    private void DelayedDexLog()
    {
        float dexterityValue = statManager.stats[StatManager.StatType.Dexterity].GetValue();
        Debug.Log("Dexterity Value: " + dexterityValue);
    }
}
