using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    public GameObject itemSlotPrefab;
    
    public GameObject gridContainer;

    private int numberOfSlots = 28;
    
    public List<InventorySlot> slots;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject gobj = Instantiate(itemSlotPrefab, gridContainer.transform);
            
            InventorySlot inventorySlot = gobj.GetComponent<InventorySlot>();
            
            inventorySlot.index = i;
            
            slots.Add(inventorySlot);
        }
    }

    public void AddItem(EquipmentInstance equipmentInstance)
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            // Check if this slot is available
            if (!slots[i].isOccupied)
            {
                // Add and return on first free.
                slots[i].AddItem(equipmentInstance);
                return;
            }
        }
    }

    public void RemoveItem(EquipmentInstance equipmentInstance)
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (slots[i].isOccupied)
            {
                if (slots[i].equipmentInstance == equipmentInstance)
                {
                    slots[i].ResetSlot();
                    slots.RemoveAt(i);
                    
                    return;
                }
            }
        }
    }

    public void RemoveItems()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (slots[i].isOccupied)
            {
                slots[i].ResetSlot();
            }
        }
    }
}
