using System;
using UnityEngine;

public static class EquipmentHelpers
{
    public enum EquipmentSlot
    {
        // Weapon
        Weapon,
        Offhand,
        
        // Armor
        Head,
        Cape,
        Shoulder,
        Chest,
        Legs,
        Belt,
        Gloves,
        Feet,
        
        // Accessories
        Eye, // Upper part of the face (glasses)
        Face, // Lower part of the face (face mask/bandana)
        Pocket,
        
        // Jewelry
        Ring1,
        Ring2,
        //Ring3,
        //Ring4,
        Amulet, // Pendant
        //Amulet2,
        Earring,
        
        Ammo
    }
    
    /// <summary>
    /// Get the equipment slot this item is valid for
    /// </summary>
    /// <param name="equipment">The equipment to check</param>
    /// <returns>The valid equipment slot</returns>
    public static EquipmentSlot GetEquipmentSlot(EquipmentBase equipment)
    {
        switch (equipment.equipmentType)
        {
            case EquipmentBase.EquipmentType.Weapon:
            {
                // 2H weapons will need to be checked here and somehow check off both Weapon and Offhand.
                return EquipmentSlot.Weapon;
            }

            case EquipmentBase.EquipmentType.OffHand:
            {
                // Check if we have a 2h equipped, if we do, we need to remove it.
                return EquipmentSlot.Offhand;
            }

            case EquipmentBase.EquipmentType.Armor:
            {
                ArmorBase armorBase = (ArmorBase)equipment;
                
                switch (armorBase.armorType)
                {
                    case ArmorBase.ArmorType.Helmet:
                        return EquipmentSlot.Head;
                    case ArmorBase.ArmorType.Cape:
                        return EquipmentSlot.Cape;
                    case ArmorBase.ArmorType.Shoulder:
                        return EquipmentSlot.Shoulder;
                    case ArmorBase.ArmorType.Chest:
                        return EquipmentSlot.Chest;
                    case ArmorBase.ArmorType.Legs:
                        return EquipmentSlot.Legs;
                    case ArmorBase.ArmorType.Belt:
                        return EquipmentSlot.Belt;
                    case ArmorBase.ArmorType.Gloves:
                        return EquipmentSlot.Gloves;
                    case ArmorBase.ArmorType.Boots:
                        return EquipmentSlot.Feet;
                }

                return default;
            }

            case EquipmentBase.EquipmentType.Jewelry:
            {
                JewelryBase jewelry = (JewelryBase)equipment;

                switch (jewelry.jewelryType)
                {
                    case JewelryBase.JewelryType.Ring:
                        return EquipmentSlot.Ring1;
                    case JewelryBase.JewelryType.Amulet:
                        return EquipmentSlot.Amulet;
                    case JewelryBase.JewelryType.Earring:
                        return EquipmentSlot.Earring;
                }
                
                return default;
            }

            default:
            {
                Debug.LogWarning($"Unknown equipment type: {equipment.itemType}");
                break;
            }
        }

        return default;
    }

    public static bool IsTwoHanded(WeaponBase.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponBase.WeaponType.TwoHandedAxe:
            case WeaponBase.WeaponType.TwoHandedSword:
            case WeaponBase.WeaponType.TwoHandedMace:
            case WeaponBase.WeaponType.Bow:
            case WeaponBase.WeaponType.Crossbow:
            case WeaponBase.WeaponType.Claw:
            case WeaponBase.WeaponType.Spear:
            case WeaponBase.WeaponType.Polearm:
            case WeaponBase.WeaponType.Staff:
            case WeaponBase.WeaponType.Gun:
                return true;
            default:
                return false;
        }
    }
}
