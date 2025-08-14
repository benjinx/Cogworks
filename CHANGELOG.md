# Change Log

## 0.0.3
Addition of the equipment system

Can create a scriptable object of an item (with stats), add it to an inventory, and equip an item.

Example can be used by adding "Warrior" and "Canvas" from the example photo to the scene and drop a reference from warrior on the player reference on "Stat Debugger UI" on the canvas in scene.

- Added: 
    - Scriptable objects - ItemBase, EquipmentBase, Various equipment types (ArmorBase, JewelryBase, OffHandBase, WeaponBase)
    - EquipmentManager, EquipmentInstance, EquipmentHelpers
    - Some example equipment
    - Basic UI for an inventory and equipment
    - Stat UI
    - Naughty Attributes (https://github.com/dbrizov/NaughtyAttributes)

## 0.0.2
Project structure was changed, all folders came up one level.

Attributes folder was moved to Stats.

Changes to Stat:
- Stats now know what type of stat they are, this enum can be found in the StatManager, it has defaults for what you may want to use.

- Added a permanent value to stats as well, this is the value that's actually modified via a levelup, talent, or a permanent effect instead of the base value. This let's you be able to add/remove stats easier.

- Added: OnStatChanged, IncreaseByPercentage(int value), DecreaseByPercentage(int value), and RemoveAllModifiersFromSource(object source).

- GetValue updated to modify the final stat based on how the stat was modified, either flat, percentAdditive, or percentMultiplicative.

Changes to StatManager:
- StatType defined, mentioned before in changes to stat, this will allow us to track the stat more properly.

- ModifyStat changed to AddStatModifier.

Changes to StatModifier:
- Added modifierName, description, statType, statModifierType, source, and onExpire.


## 0.0.1

Initial project structure and implementation of the following systems:

### Core
---

#### Object Pool
- IPooledObject
- ObjectPool

#### State Machine
- IdleState
- State
- StateMachine
- Examples

### Gameplay
---

#### Attributes
- LevelSystem
- Stat
- StatManager
- StatModifier
- Examples

#### Hitbox
- Hitbox
- Examples

#### Resources
- Health
- Mana
- Stamina

### Utility
---
- AnimationCurveEvaluator
- DatatypeHelpers
- DontDestoryOnLoad
- SplineEvaluator
- Timer

