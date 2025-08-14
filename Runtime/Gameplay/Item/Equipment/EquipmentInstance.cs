using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class EquipmentInstance
{
    public EquipmentBase equipmentBase;
    public List<StatModifier> statModifiers = new List<StatModifier>();

    public EquipmentInstance(EquipmentBase equipment)
    {
        equipmentBase = equipment;
        statModifiers = equipment.statModifiers.Select(m => m.CloneWithNewSource(this)).ToList();
    }
}
