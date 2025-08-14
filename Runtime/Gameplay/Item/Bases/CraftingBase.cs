using UnityEngine;

[CreateAssetMenu(fileName = "CraftingBase", menuName = "Scriptable Objects/Items/CraftingBase")]
public class CraftingBase : ItemBase
{
    public string effectDescription;
    public int stackSize = 1;
    public int maxStackSize = 50;

    public CraftingBase()
    {
        itemType = ItemType.Crafting;
    }
}
