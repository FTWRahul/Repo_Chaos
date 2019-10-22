using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public TypeSize typeSize;

    public List<Shelf> shelves;

    public void Inti()
    {
        List<Item> myItems = new List<Item>();
        foreach (Item item in ItemsDatabase.Instance.database.Values)
        {
            if (item.typeSize == typeSize)
            {
                myItems.Add(item);
            }
        }
        SpawnItemsInShelves(myItems);
    }

    public void SpawnItemsInShelves(List<Item> items)
    {
        for (int i = 0; i < shelves.Count; i++)
        {
            int rand = Random.Range(0, items.Count);
            shelves[i].GetComponent<Shelf>().GenerateItems(items[rand]);
        }
    }
}

