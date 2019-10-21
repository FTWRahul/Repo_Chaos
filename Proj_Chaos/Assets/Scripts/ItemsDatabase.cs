using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public static ItemsDatabase Instance { get; private set; }
    
    public Dictionary<int, Item> database = new Dictionary<int, Item>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        Init();
    }
    
    private void Init()
    { 
        Object[] items = Resources.LoadAll("Items", typeof(ItemSO));
        
        // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
        foreach (ItemSO item in items)
        {
            Item newItem = new Item(item);
            database.Add(newItem.itemId, newItem);
        }
    }
}
