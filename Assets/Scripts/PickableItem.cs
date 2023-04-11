using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickableItem : MonoBehaviour
{
    [SerializeField] bool automaticallyPickedUp = false;
    [SerializeField] Sprite itemIconSprite;
    [SerializeField] string itemNameInInventory = "PLACEHOLDER_NAME";
    [SerializeField] string itemDescriptionInInventory = "PLACEHOLDER_DESCRIPTION PLACEHOLDER_DESCRIPTION PLACEHOLDER_DESCRIPTION PLACEHOLDER_DESCRIPTION";

    string itemTag;

    [Header("For Healing Items Only")]
    [SerializeField] int healAmount = 10;

    [Header("For Weapon Items Only")]



    int slot_num = -1; // WILL BE BETWEEN 0-29 IF SLOTTED, -1 IF NOT

    // Start is called before the first frame update
    void Start()
    {
        itemTag = tag;
        //Debug.Log(itemTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (slot_num == -1 && automaticallyPickedUp)
        {
            PickUpItem();
        }
        if (slot_num > -1) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }

    public void PickUpItem()
    {
        FindObjectOfType<InventoryCanvas>().AddItemToInventory(this);
        //Destroy(gameObject);
    }

    public Sprite GetItemIcon() { return itemIconSprite; }
    //public void SetItemIcon(Sprite sprt) { itemIconSprite = sprt; }
     
    public void UseItem()
    {
        //FindObjectOfType<InventoryCanvas>().GetSlotAtIndex(slot_num).ClearIconImage();
        if (itemTag == "HealingItem") 
        {
            //Debug.Log(gameObject.name + "HAS BEEN USED TO HEAL");
            FindObjectOfType<PlayerController>().GetComponent<Health>().Heal(healAmount);
            Destroy(gameObject);
        }
        else if (itemTag == "WeaponItem") 
        { 
            Debug.Log("IS WEAPON ITEM"); 
        }
        
    }

    public void SetSlotNum(int n)
    {
        slot_num = n;
    }

    public int GetSlotNum()
    {
        return slot_num;
    }

    public string GetItemInventoryName()
    {
        return itemNameInInventory;
    }

    public string GetItemInventoryDescription()
    {
        return itemDescriptionInInventory;
    }
}
