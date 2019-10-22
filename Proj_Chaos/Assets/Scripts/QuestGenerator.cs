using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestGenerator : MonoBehaviour
{
    private List<int> _itemsToCollect = new List<int>();
    public int maxItemAmount;

    private void Start()
    {
        for (int i = 0; i < maxItemAmount; i++)
        {
            int rand = Random.Range(0, ItemsDatabase.Instance.database.Count);
            _itemsToCollect.Add(ItemsDatabase.Instance.database[rand].itemId);
        }
    }
}
