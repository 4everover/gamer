using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCanvas : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject DeathScreen;

    [SerializeField] GameObject inventory;

    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemDescriptionText;

    [SerializeField] PickableItem[] pickableItemsList; // SERIALIZED FOR DEBUGGING ONLY
    [SerializeField] ItemSlot[] itemSlotsList;

    // Start is called before the first frame update
    void Start()
    {
        pickableItemsList = new PickableItem[itemSlotsList.Length];
        for (int n = 0; n< pickableItemsList.Length; n++) 
        { 
            pickableItemsList[n] = null;
            itemSlotsList[n].SetSlotNumber(n);
        }

        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventory();

        if (PauseScreen.activeInHierarchy || DeathScreen.activeInHierarchy)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Debug.Log("I pressed");
            if (inventory.activeSelf) { HideInventory(); }
            else { ShowInventory(); }
            
        }
    }

    public void AddItemToInventory(PickableItem item)
    {
        for (int n=0; n< pickableItemsList.Length; n++)
        {
            if (pickableItemsList[n] == null) 
            {
                item.SetSlotNum(n);
                pickableItemsList[n] = item;
                itemSlotsList[n].SetIconImage(pickableItemsList[n].GetItemIcon());
                //Debug.Log("ADDED " + item.name + " TO SLOT " + n);
                break;
            }
        }
        //Debug.Log("ITEM ADDED TO INVENTORY: " + pickableItemsList[0].name);
    }

    public void UpdateInventory()
    {
        for (int n = 0; n < pickableItemsList.Length; n++)
        {
            if (pickableItemsList[n] == null)
            {
                itemSlotsList[n].ClearIconImage();
            }
        }
    }

    public void HandleItemUseAtIndex(int i)
    {
        //itemSlotsList[n].ClearIconImage();
        pickableItemsList[i].UseItem();
    }

    public void ShowInventory()
    {
        Time.timeScale = 0;
        inventory.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideInventory()
    {
        Time.timeScale = 1;
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public ItemSlot GetSlotAtIndex(int i)
    {
        return itemSlotsList[i];
    }

    public void SetItemDescriptionBoxText(int slotNum)
    {
        if (slotNum == -1)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
        }
        else
        {
            itemNameText.text = pickableItemsList[slotNum].GetItemInventoryName();
            itemDescriptionText.text = pickableItemsList[slotNum].GetItemInventoryDescription();
        }
    }
}
