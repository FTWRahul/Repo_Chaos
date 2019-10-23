using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : Singleton<ItemsDatabase>
{
    public Dictionary<int, Item> database = new Dictionary<int, Item>();
    public Dictionary<int, int> objectsInScene = new Dictionary<int, int>();

    public GameObject playerPrefab;
    public Transform spawnLocation;
    public GameObject NPCSpawnner;

    protected override void Awake()
    {
        base.Awake();
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

        Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        NPCSpawnner.SetActive(true);
    }
}
