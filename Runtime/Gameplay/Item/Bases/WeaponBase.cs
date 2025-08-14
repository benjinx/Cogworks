using UnityEngine;

[CreateAssetMenu(fileName = "WeaponBase", menuName = "Scriptable Objects/Equipment/WeaponBase")]
public class WeaponBase : EquipmentBase
{
    public enum WeaponType
    {
        OneHandedAxe,
        TwoHandedAxe,
        OneHandedSword,
        TwoHandedSword,
        OneHandedMace,
        TwoHandedMace,
        Bow,
        Crossbow,
        Claw,
        Dagger,
        Spear,
        Polearm,
        Wand,
        Staff,
        Knuckle,
        Gun,
    }
    
    public enum AttackSpeedType
    {
        Slow,
        Normal,
        Fast,
    }
    
    public WeaponType weaponType;
    
    public AttackSpeedType attackSpeed = AttackSpeedType.Normal;

    public int attackPower;

    public WeaponBase()
    {
        equipmentType = EquipmentType.Weapon;
    }
}
