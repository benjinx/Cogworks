using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    private EquipmentManager equipmentManager;

    public Inventory inventory;

    public List<EquipmentBase> equipment = new List<EquipmentBase>();
    
    void Awake()
    {
        equipmentManager = gameObject.GetComponent<EquipmentManager>();
    }

    private void Start()
    {
        // Add equipment to inventory at start
        foreach (EquipmentBase equip in equipment)
        {
            inventory.AddItem(new EquipmentInstance(equip));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            // Remove the first item, we really need something better
            inventory.RemoveItems();
        }
    }
}
