using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public void TeleportPlayer()
    {
        FindObjectOfType<InventoryCanvas>().HideInventory();
        FindObjectOfType<PlayerController>().transform.position = transform.position;
    }
}
