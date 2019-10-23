using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemId;
    public string itemName;
    public GameObject itemPrefab;
    public TypeSize typeSize;
    public Texture2D boxArt;
    public Texture2D ShelfArt1;
    public Texture2D ShelfArt2;
    public Texture2D ShelfArt3;
    public Texture2D ShelfArt4;
    
    public Item(ItemSO item)
    {
        itemId = item.itemId;
        itemName = item.itemName;
        itemPrefab = item.itemPrefab;
        typeSize = item.typeSize;
        boxArt = item.boxArt;
        ShelfArt1 = item.ShelfArt1;
        ShelfArt2 = item.ShelfArt2;
        ShelfArt3 = item.ShelfArt3;
        ShelfArt4 = item.ShelfArt4;
    }
}
