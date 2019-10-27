using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestGenerator : MonoBehaviour
{
    [HideInInspector] public Events.EventQuestDone onQuestDone;
    public List<int> itemsToCollect = new List<int>();
    [SerializeField] private int maxItemAmount;

    private void Awake()
    {
        for (int i = 0; i < maxItemAmount; i++)
        {
            int random = Random.Range(0, ItemsDatabase.Instance.database.Count);
            
            if (!itemsToCollect.Exists(x => x == random))
            {
                itemsToCollect.Add(ItemsDatabase.Instance.database[random].itemId);
            }
            else
            {
                maxItemAmount++;
            }
        }
    }

    private void Update()
    {
        if (itemsToCollect.Count <= 0)
        {
            onQuestDone?.Invoke();
        }
    }
}
