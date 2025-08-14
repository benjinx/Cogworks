using UnityEngine;

[CreateAssetMenu(fileName = "OffHandBase", menuName = "Scriptable Objects/Equipment/OffHandBase")]
public class OffHandBase : EquipmentBase
{
    public enum OffHandType
    {
        // Off Hand
        // https://maplestory.fandom.com/wiki/Category:Secondary_Weapons
        Quiver,
        Shield,
        MagicBook,
        Magazine, // Only if gun makes it
    }
    
    public OffHandType offHandType;

    public OffHandBase()
    {
        equipmentType = EquipmentType.OffHand;
    }
}
