using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemSlot : MonoBehaviour
{
    // Need to assign what equipment slot this is
    public EquipmentHelpers.EquipmentSlot equipmentSlot;
    
    public bool isOccupied = false;
    
    public EquipmentInstance equipmentInstance;
    
    public Image image;
    
    private Sprite cachedIcon;
    
    public Button button;

    public TextMeshProUGUI tmp;

    void Awake()
    {
        image = GetComponent<Image>();
        cachedIcon = image.sprite;

        button = GetComponent<Button>();
        button.enabled = false;
        
        tmp = button.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void Start()
    {
        EquipmentManager.instance.OnEquipped.AddListener(EquipItem);
        EquipmentManager.instance.OnUnequipped.AddListener(UnequipItem);
    }

    private void EquipItem(EquipmentInstance equipmentInstance)
    {
        EquipmentHelpers.EquipmentSlot slotToCheck = EquipmentHelpers.GetEquipmentSlot(equipmentInstance.equipmentBase);
        
        if (slotToCheck == EquipmentHelpers.EquipmentSlot.Ring1)
        {
            slotToCheck = EquipmentManager.instance.RingSlotCheck(equipmentInstance);

            if (slotToCheck == EquipmentHelpers.EquipmentSlot.Ring2 &&
                equipmentSlot != EquipmentHelpers.EquipmentSlot.Ring2) // This makes sure the 2nd ring equipment
                // slot does not attempt to call itself
            {
                // Call into the Ring2 EquipmentItemSlot
                GameObject gobj = GameObject.Find("Ring 2");
                    
                EquipmentItemSlot itemSlot = gobj.GetComponent<EquipmentItemSlot>();
                    
                itemSlot.EquipItem(equipmentInstance);
                    
                return;
            }
        }
        
        // Check if this slot can hold this equipment type
        if (equipmentSlot == slotToCheck)
        {
            isOccupied = true;
            this.equipmentInstance = equipmentInstance;
            image.sprite = equipmentInstance.equipmentBase.icon;
            button.enabled = true;
            tmp.enabled = false;
        }
    }

    private void UnequipItem(EquipmentInstance equipmentInstance)
    {
        // Not valid to unequip
        if (equipmentInstance == null ||
            equipmentInstance.equipmentBase == null)
        {
            return;
        }

        // Not our item
        if (this.equipmentInstance != equipmentInstance)
        {
            return;
        }
        
        EquipmentHelpers.EquipmentSlot slotToCheck = EquipmentHelpers.GetEquipmentSlot(equipmentInstance.equipmentBase);

        if (slotToCheck == EquipmentHelpers.EquipmentSlot.Ring1)
        {
            GameObject ring1 = GameObject.Find("Ring 1");
            EquipmentItemSlot ring1ItemSlot = ring1.GetComponent<EquipmentItemSlot>();
            
            GameObject ring2 = GameObject.Find("Ring 2");
            EquipmentItemSlot ring2ItemSlot = ring2.GetComponent<EquipmentItemSlot>();

            if (equipmentInstance == ring1ItemSlot.equipmentInstance)
            {
                // Remove ring 1
                slotToCheck = EquipmentHelpers.EquipmentSlot.Ring1;
            }
            else if (equipmentInstance == ring2ItemSlot.equipmentInstance)
            {
                // Remove ring 2
                slotToCheck = EquipmentHelpers.EquipmentSlot.Ring2;
            }
        }
        
        // Check if this slot can hold this equipment type
        if (equipmentSlot == slotToCheck)
        {
            isOccupied = false;
            this.equipmentInstance = null;
            image.sprite = cachedIcon;
            button.enabled = false;
            tmp.enabled = true;
            
            Inventory.instance.AddItem(equipmentInstance);
        }
    }

    public void UnequipItem()
    {
        EquipmentManager.instance.Unequip(equipmentInstance);
    }
}
