using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public List<Transform> smallItemPosition;
    public List<Transform> mediumItemPosition;
    public List<Transform> largeItemPosition;

    public void GenerateItems(Item item)
    {
        if (item.typeSize == TypeSize.SMALL)
        {
            PlaceItem(smallItemPosition, item);
        }
        else if (item.typeSize == TypeSize.MEDIUM)
        {
            PlaceItem(mediumItemPosition, item);
        }
        else if (item.typeSize == TypeSize.LARGE)
        {
            PlaceItem(largeItemPosition, item);
        }
    }

    public void PlaceItem(List<Transform> positions, Item item)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            ItemId tempGo = Instantiate(item.itemPrefab, positions[i].position, transform.rotation).GetComponent<ItemId>();
            tempGo.transform.parent = this.transform;
            tempGo.Init(item.itemId);
            
            if(ItemsDatabase.Instance.objectsInScene.ContainsKey(item.itemId))
            {
                ItemsDatabase.Instance.objectsInScene[item.itemId]++;
            }
            else
            {
                ItemsDatabase.Instance.objectsInScene.Add(item.itemId, 1);
            }
        }
    }
}

