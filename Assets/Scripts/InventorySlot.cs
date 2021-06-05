using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;

    [HideInInspector]public int id = -1; // -1 = free

    public void UpdateInventory(ItemObject item) {
        image.sprite = item.uiSprite;
        id = item.id;
    }
}
