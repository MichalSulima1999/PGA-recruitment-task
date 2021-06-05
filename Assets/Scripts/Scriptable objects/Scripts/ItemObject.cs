using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Key
}

public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite uiSprite;
    public ItemType type;
    public string description;
    public Color hoverColor;
}
