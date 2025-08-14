using UnityEngine;
using UnityEngine.Serialization;

public class WarriorStatManager : MonoBehaviour
{
    private StatManager statManager;

    private void Awake()
    {
        statManager = gameObject.GetComponent<StatManager>();
        
        // Example of level up might automatically do this, or player chose to increase this via point distribution
        statManager.stats[StatManager.StatType.Strength].IncreasePermanent(2);

        // Equip an item that gives +5 strength
        statManager.AddStatModifier(
            "Warrior Talent",
            "The warrior inherits features from his father granting +5 strength.",
            StatManager.StatType.Strength,
            5,
            StatModifier.StatModifierType.Flat,
            this
            );

        // Apply a temporary +2 dexterity for 5 seconds
        statManager.AddStatModifier(
            "Agile Buff",
            "You're quicker for a short period of time.",
            StatManager.StatType.Dexterity,
            2,
            StatModifier.StatModifierType.Flat,
            this,
            true,
            5.0f
            );
    }
}
