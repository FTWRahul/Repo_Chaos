using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemId;
    public string itemName;
    public GameObject itemPrefab;
    public TypeSize typeSize;
    
    public Item(ItemSO item)
    {
        itemId = item.itemId;
        itemName = item.itemName;
        itemPrefab = item.itemPrefab;
        typeSize = item.typeSize;
    }
}
