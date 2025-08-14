using System.Collections.Generic;
using NaughtyAttributes;

public class EquipmentBase : ItemBase
{
    public enum EquipmentType
    {
        Weapon,
        OffHand,
        Armor,
        Jewelry,
    }
    
    public EquipmentBase()
    {
        itemType = ItemType.Equipment;
        classRequirement = EquipmentManager.CharacterClass.Warrior |
                           EquipmentManager.CharacterClass.Magician |
                           EquipmentManager.CharacterClass.Bowmen |
                           EquipmentManager.CharacterClass.Thieves |
                           EquipmentManager.CharacterClass.Pirate;
    }
    
    public EquipmentType equipmentType;

    [EnumFlags]
    public EquipmentManager.CharacterClass classRequirement;
    
    public int levelRequirement;
    
    public int strengthRequirement;

    public int dexterityRequirement;

    public int intelligenceRequirement;

    public List<StatModifier> statModifiers;
}
