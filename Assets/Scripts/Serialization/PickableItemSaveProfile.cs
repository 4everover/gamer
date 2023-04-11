using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickableItemSaveProfile
{
    public float currentPositionX;
    public float currentPositionY;
    public float currentPositionZ;
    //public ItemType itemType;
    //public string itemTag;
    public string slotNum;

    public string id;

    /*public PickableItemSaveProfile(PickableItem item)
    {
        currentPosition = new float[3];
        currentPosition[0] = item.transform.position.x;
        currentPosition[1] = item.transform.position.y;
        currentPosition[2] = item.transform.position.z;
    }*/
}
