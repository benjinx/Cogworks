using NaughtyAttributes;
using UnityEngine;

//[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Consumable,
        Crafting,
        Quest,
        Currency,
        Socketable
    }

    public enum RarityType
    {
        Normal,
        Magic,
        Rare,
        Unique
    }
    
    [ShowAssetPreview]
    public Sprite icon;
    
    public string itemName;
    
    public string itemDescription;
    
    public RarityType rarityType;
    
    public ItemType itemType;
}
