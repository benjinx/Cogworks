using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    
    public Image image;

    private Sprite cachedSprite;

    private Button button;

    public int index;
    
    public bool isOccupied = false;
    
    public EquipmentInstance equipmentInstance;
    
    void Awake()
    {
        image = GetComponent<Image>();

        cachedSprite = image.sprite;
        
        button = GetComponent<Button>();
        button.enabled = false;
    }

    public void AddItem(EquipmentInstance equipmentInstance)
    {
        image.sprite = equipmentInstance.equipmentBase.icon;
        
        isOccupied = true;
        
        this.equipmentInstance = equipmentInstance;

        button.enabled = true;
    }

    public void ResetSlot()
    {
        this.image.sprite = cachedSprite;
        
        isOccupied = false;
        
        button.enabled = false;
    }

    public void EquipItem()
    {
        EquipmentManager.instance.Equip(equipmentInstance);
       
        ResetSlot();
    }
}
