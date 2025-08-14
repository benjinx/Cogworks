using UnityEngine;

[CreateAssetMenu(fileName = "JewelryBase", menuName = "Scriptable Objects/Equipment/JewelryBase")]
public class JewelryBase : EquipmentBase
{
    public enum JewelryType
    {
        Ring,
        Amulet,
        Earring
    }
    
    public JewelryType jewelryType;

    public JewelryBase()
    {
        equipmentType = EquipmentType.Jewelry;
    }
}
