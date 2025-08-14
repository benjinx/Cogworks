using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorBase", menuName = "Scriptable Objects/Equipment/ArmorBase")]
public class ArmorBase : EquipmentBase
{
    public enum ArmorType
    {
        Helmet,
        Cape,
        Shoulder,
        Chest,
        Legs,
        Belt,
        Gloves,
        Boots
    }
    
    public ArmorType armorType;

    public ArmorBase()
    {
        equipmentType = EquipmentType.Armor;
    }
}
