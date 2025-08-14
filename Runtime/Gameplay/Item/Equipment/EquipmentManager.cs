using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    
    public enum CharacterClass
    {
        None = 0,
        Warrior = 1 << 0,
        Magician = 1 << 1,
        Bowmen = 1 << 2,
        Thieves = 1 << 3,
        Pirate = 1 << 4,
    }
    
    public CharacterClass characterClass;
    
    private StatManager statManager;
    
    private LevelSystem levelSystem;
    
    public SerializedDictionary<EquipmentHelpers.EquipmentSlot, EquipmentInstance> equippedItems =
        new SerializedDictionary<EquipmentHelpers.EquipmentSlot, EquipmentInstance>();

    public UnityEvent<EquipmentInstance> OnEquipped = new UnityEvent<EquipmentInstance>();
    public UnityEvent<EquipmentInstance> OnUnequipped = new UnityEvent<EquipmentInstance>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        statManager = gameObject.GetComponent<StatManager>();
        levelSystem = gameObject.GetComponent<LevelSystem>();
    }
    
    public void Equip(EquipmentInstance equipmentInstance)
    {
        EquipmentHelpers.EquipmentSlot slot = EquipmentHelpers.GetEquipmentSlot(equipmentInstance.equipmentBase);
        
        if (!ValidRequirementCheck(equipmentInstance))
        {
            return;
        }
        
        if (equipmentInstance.equipmentBase.equipmentType == EquipmentBase.EquipmentType.Jewelry)
        {
            JewelryBase jewelry = (JewelryBase)equipmentInstance.equipmentBase;

            if (jewelry.jewelryType == JewelryBase.JewelryType.Ring)
            {
                if (!equippedItems.ContainsKey(EquipmentHelpers.EquipmentSlot.Ring1))
                {
                    slot = EquipmentHelpers.EquipmentSlot.Ring1;
                }
                else if (!equippedItems.ContainsKey(EquipmentHelpers.EquipmentSlot.Ring2))
                {
                    slot = EquipmentHelpers.EquipmentSlot.Ring2;
                }
                else
                {
                    Unequip(equippedItems[EquipmentHelpers.EquipmentSlot.Ring1]);
                }
            }
        }

        if (equipmentInstance.equipmentBase.equipmentType == EquipmentBase.EquipmentType.Weapon)
        {
            WeaponBase weapon = (WeaponBase)equipmentInstance.equipmentBase;
            
            if (EquipmentHelpers.IsTwoHanded(weapon.weaponType))
            {
                if (equippedItems.TryGetValue(EquipmentHelpers.EquipmentSlot.Offhand, out EquipmentInstance offhand))
                {
                    Unequip(offhand);
                }
            }
        }
        else if (equipmentInstance.equipmentBase.equipmentType == EquipmentBase.EquipmentType.OffHand)
        {
            if (equippedItems.TryGetValue(EquipmentHelpers.EquipmentSlot.Weapon, out EquipmentInstance weapon))
            {
                WeaponBase weaponBase = (WeaponBase)weapon.equipmentBase;
                
                if (EquipmentHelpers.IsTwoHanded(weaponBase.weaponType))
                {
                    Unequip(weapon);
                }
            }
        }

        // Unequip any item if the slot has any
        if (equippedItems.TryGetValue(slot, out EquipmentInstance existingItem))
        {
            Unequip(existingItem);
        }

        // Equip the item
        equippedItems[slot] = equipmentInstance;
        
        foreach (StatModifier statModifier in equipmentInstance.statModifiers)
        {
            statManager.AddStatModifier(statModifier);
        }
        
        OnEquipped?.Invoke(equipmentInstance);
    }

    public EquipmentHelpers.EquipmentSlot RingSlotCheck(EquipmentInstance equipmentInstance)
    {
        if (equippedItems.TryGetValue(EquipmentHelpers.EquipmentSlot.Ring1, out EquipmentInstance ring1))
        {
            if (ring1 == equipmentInstance)
            {
                return EquipmentHelpers.EquipmentSlot.Ring1;
            }
        }

        if (equippedItems.TryGetValue(EquipmentHelpers.EquipmentSlot.Ring2, out EquipmentInstance ring2))
        {
            if (ring2 == equipmentInstance)
            {
                return EquipmentHelpers.EquipmentSlot.Ring2;
            }
        }

        // Really need to make sure we don't hit this? hmm
        return 0;
    }

    private bool ValidRequirementCheck(EquipmentInstance equipmentInstance)
    {
        EquipmentBase equipmentBase = equipmentInstance.equipmentBase;
        
        // Check allowed class
        if ((equipmentBase.classRequirement & characterClass) == 0)
        {
            return false;
        }
        
        // Check level
        if (levelSystem.level < equipmentBase.levelRequirement)
        {
            return false;
        }

        // Check Primary Stats
        if (statManager.stats.ContainsKey(StatManager.StatType.Strength))
        {
            if (statManager.stats[StatManager.StatType.Strength].GetValue() <
                equipmentBase.strengthRequirement)
            {
                return false;
            }
        }

        if (statManager.stats.ContainsKey(StatManager.StatType.Dexterity))
        {
            if (statManager.stats[StatManager.StatType.Dexterity].GetValue() <
                equipmentBase.dexterityRequirement)
            {
                return false;
            }
        }

        if (statManager.stats.ContainsKey(StatManager.StatType.Intellect))
        {
            if (statManager.stats[StatManager.StatType.Intellect].GetValue() <
                equipmentBase.intelligenceRequirement)
            {
                return false;
            }
        }

        return true;
    }
    
    public void Unequip(EquipmentInstance equipmentInstance)
    {
        var slot = equippedItems.FirstOrDefault(kv => kv.Value == equipmentInstance).Key;
        
        if (!slot.Equals(default))
        {
            equippedItems.Remove(slot);
            
            foreach (var stat in statManager.stats.Values)
            {
                stat.RemoveAllModifiersFromSource(equipmentInstance);
            }
        }
        
        OnUnequipped?.Invoke(equipmentInstance);
    }

    public EquipmentInstance GetEquippedItem(EquipmentHelpers.EquipmentSlot slot)
    {
        return equippedItems.TryGetValue(slot, out EquipmentInstance item) ? item : null;
    }

    public Dictionary<EquipmentHelpers.EquipmentSlot, EquipmentInstance> GetAllEquippedItems()
    {
        return equippedItems;
    }
    
}
