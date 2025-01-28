using UnityEngine;

public class StatManagerExample : MonoBehaviour
{
    // The stats this character will have
    public enum StatName
    {
        Strength,
        Dexterity,
        Vitality,
        Energy
    };

    private StatManager statManager;

    private void Start()
    {
        statManager = gameObject.AddComponent<StatManager>();

        // Set inital stat values
        statManager.stats[StatName.Strength.ToString()] = new Stat(10);
        statManager.stats[StatName.Dexterity.ToString()] = new Stat(8);
        statManager.stats[StatName.Vitality.ToString()] = new Stat(6);
        statManager.stats[StatName.Energy.ToString()] = new Stat(4);

        // Example of level up automatically does this, or player chose to increase this via points
        statManager.stats[StatName.Strength.ToString()].Increase(2);

        // Equip an item that gives +5 strength
        statManager.ModifyStat(StatName.Strength.ToString(), 5);

        // Apply a temporary +2 dexterity for 5 seconds
        statManager.ModifyStat(StatName.Dexterity.ToString(), 2, true, 5.0f);

        // Get current strength value
        float strengthValue = statManager.stats[StatName.Strength.ToString()].GetValue();
        Debug.Log("Strength Value: " + strengthValue);

        float dexterityValue = statManager.stats[StatName.Dexterity.ToString()].GetValue();
        Debug.Log("Dexterity Value: " + dexterityValue);

        Debug.Log("Please wait 5 seconds for duration...");
        Invoke(nameof(DelayedDexLog), 5.0f);
    }

    private void DelayedDexLog()
    {
        float dexterityValue = statManager.stats[StatName.Dexterity.ToString()].GetValue();
        Debug.Log("Dexterity Value: " + dexterityValue);
    }
}
