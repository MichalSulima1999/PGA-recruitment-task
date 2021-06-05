using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] invSlots;

    public static float InteractRange = 5f;
    public static LayerMask whatIsPlayer;

    // Start is called before the first frame update
    void Start()
    {
        whatIsPlayer = LayerMask.GetMask("Player");
    }

    public bool SetItem(ItemObject item)
    {
        // check if slot is free ( id < 0 - free )
        foreach (InventorySlot invSlot in invSlots) {
            if(invSlot.id < 0) {
                invSlot.UpdateInventory(item);
                return true;
            }
        }
        return false;
    }
}
