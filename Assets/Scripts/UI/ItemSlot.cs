using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemIconImage;
    bool imageIsSet;
    int slot_num = -1; // RANGES FROM 0-29, IS -1 IF UNASSIGNED

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIconImage(Sprite sprt)
    {
        itemIconImage.sprite = sprt;
        //Debug.Log("ICON COLOR SET");
        itemIconImage.color = new Color(1, 1, 1, 1);
        imageIsSet = true;
    }

    public void ClearIconImage()
    {
        itemIconImage.sprite = null;
        itemIconImage.color = new Color(0, 0, 0, 0);
    }

    public bool GetIconSet()
    {
        return imageIsSet;
    }

    public void SetSlotNumber(int inputNum)
    {
        slot_num = inputNum;
    }

    public int GetSlotNumber()
    {
        return slot_num;
    }

    public void ReactToButtonClick()
    {
        if (itemIconImage.sprite) FindObjectOfType<InventoryCanvas>().HandleItemUseAtIndex(slot_num);
        FindObjectOfType<InventoryCanvas>().SetItemDescriptionBoxText(-1);
    }

    public void ReactToMouseHover()
    {
        if (itemIconImage.sprite) FindObjectOfType<InventoryCanvas>().SetItemDescriptionBoxText(slot_num);
        
        else FindObjectOfType<InventoryCanvas>().SetItemDescriptionBoxText(-1);
    }
}
